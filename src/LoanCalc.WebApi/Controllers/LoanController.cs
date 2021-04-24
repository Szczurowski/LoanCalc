using System;
using System.Collections.Generic;
using LoanCalc.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanCalc.WebApi.Controllers
{
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;

        public LoanController(ILogger<LoanController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Housing/amount/{amount}/durationMonths/{duration}")]
        public IActionResult HousingDetails(decimal amount, int duration)
        {
            var validationResult = ValidateInput(amount, duration);
            if (validationResult.Count > 0)
            {
                var resultModel = ResultModel<LoanCalculationModel>.FromValidationErrors(validationResult);
                _logger.LogWarning(resultModel.ValidationErrorMessage);

                return BadRequest(resultModel);
            }

            try
            {
                // TODO: Implement
                var loanCalculationModel = new LoanCalculationModel();

                return Ok(ResultModel<LoanCalculationModel>.FromPayload(loanCalculationModel));
            }
            catch (Exception e)
            {
                var issueReference = Guid.NewGuid();
                var message = $"Processing of Housing loan with amount {amount}, duration in months {duration} crashed." +
                              $"Issue reference: {issueReference}";
                _logger.LogError(e, message);
                var resultModel = ResultModel<LoanCalculationModel>.FromError(message);
                
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
    }
}