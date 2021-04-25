using System;
using LoanCalc.Domain;

namespace LoanCalc.Engine
{
    public class CalculationEngine : ICalculationEngine
    {
        private readonly ILoanEngineConfiguration _configuration;

        public CalculationEngine(ILoanEngineConfiguration configuration)
        {
            _configuration = configuration;
        }

        // TODO: Implement
        public PaymentOverview GeneratePaymentOverview(decimal amount, int duration)
            => throw new NotImplementedException();
    }
}
