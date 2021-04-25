using System;
using System.Collections.Generic;
using LoanCalc.Engine;
using LoanCalc.WebApi.Models;
using LoanCalc.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanCalc.WebApi.Controllers
{
    public class HousingLoanController : ControllerBase
    {
        private readonly ILogger<HousingLoanController> _logger;
        private readonly ICalculationEngine _engine;
        private readonly ILoanConverter _converter;

        public HousingLoanController(
            ILogger<HousingLoanController> logger,
            ICalculationEngine engine,
            ILoanConverter converter)
        {
            _converter = converter;
            _engine = engine;
            _logger = logger;
        }

        [HttpGet]
        [Route("PaymentOverview/amount/{amount}/durationMonths/{duration}")]
        public IActionResult PaymentOverview(decimal amount, int duration)
        {
            var actionDesc = GetDescription(amount, duration);
            _logger.LogInformation($"Called {actionDesc}");

            var validationResult = ValidateInput(amount, duration);
            if (validationResult.Count > 0)
            {
                var resultModel = ResultModel<PaymentOverviewModel>.FromValidationErrors(validationResult);
                _logger.LogWarning(resultModel.GetValidationErrorMessage());

                return BadRequest(resultModel);
            }

            try
            {
                var paymentOverview = _engine.GeneratePaymentOverview(amount, duration);
                var paymentOverviewModel = _converter.Convert(paymentOverview);

                return Ok(ResultModel<PaymentOverviewModel>.FromPayload(paymentOverviewModel));
            }
            catch (Exception e)
            {
                var issueReference = Guid.NewGuid();
                var message = $"Processing of {actionDesc} crashed. Issue reference: {issueReference}";
                _logger.LogError(e, message);
                var resultModel = ResultModel<PaymentOverviewModel>.FromError(message);
                
                return base.StatusCode(StatusCodes.Status500InternalServerError, resultModel);
            }
        }

        // TODO: handle by specialized and dedicated validator
        private IReadOnlyDictionary<string, string> ValidateInput(decimal amount, int duration)
        {
            const int maxDuration = 360; // TODO: have it configured
            var result = new Dictionary<string, string>();

            if (amount <= 0)
            {
                result.Add(nameof(amount), "Needs to be greater than 0.");
            }

            if (duration <= 0m)
            {
                result.Add(nameof(duration), "Needs to be greater than 0.");
            }
            else if (duration > maxDuration)
            {
                result.Add(nameof(duration), $"Maximum of {maxDuration} months allowed.");
            }

            return result;
        }

        private static string GetDescription(decimal amount, int duration) =>
            $"{nameof(PaymentOverview)} with amount {amount}, duration in months {duration}";
    }
}