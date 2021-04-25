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

        public PaymentOverview GeneratePaymentOverview(decimal amount, int duration)
        {
            var totalAdminFees = GetAdminFee(amount);
            var monthlyPayment = GetMonthlyPayment(amount, duration);

            var totalCost = monthlyPayment * duration;
            var totalInterest = totalCost - amount;

            var actualAnnualInterestRate = GetActualAnnualInterestRate(amount, duration, totalAdminFees);

            return new PaymentOverview
            {
                ActualAnnualInterestRate = actualAnnualInterestRate,
                MonthlyCost = monthlyPayment,
                TotalAdminFees = totalAdminFees,
                TotalInterest = totalInterest
            };
        }

        private decimal GetActualAnnualInterestRate(decimal amount, int duration, decimal adminFee)
        {
            // I'm not really sure this formula is correct, would have to verify it w\ business
            // This is approximation
            var feeRatePercentage = adminFee / amount * 100M;
            var annualFeeRatePercentage = feeRatePercentage * 12M / duration;

            var result = _configuration.AnnualInterestRatePercentage + annualFeeRatePercentage;
            return result;
        }

        private decimal GetMonthlyPayment(decimal amount, int duration)
        {
            var currentInterestRate = GetMonthlyInterestRate();

            // R=A*(q^n)*(q-1)/[(q^n)-1]
            // q=1+currentInterestRate
            var qPowN = (decimal) Math.Pow((double) (1M + currentInterestRate), duration);
            var monthlyPayment = amount * qPowN * currentInterestRate / (qPowN - 1);
            // Lets take into consideration only 2 decimal places
            monthlyPayment = Math.Round(monthlyPayment, 2, MidpointRounding.AwayFromZero);

            return monthlyPayment;
        }

        private decimal GetMonthlyInterestRate()
        {
            switch (_configuration.InterestRateType)
            {
                case InterestRateType.Monthly:
                    return _configuration.AnnualInterestRatePercentage / 12M / 100M;
                case InterestRateType.Annually:
                default:
                    throw new NotImplementedException();
            }
        }

        private decimal GetAdminFee(decimal amount)
        {
            var feePercentageBased = _configuration.AdminFeePercentage * amount / 100M;

            return Math.Min(feePercentageBased, _configuration.AdminFeeAmount);
        }
    }
}
