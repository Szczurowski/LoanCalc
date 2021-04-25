using LoanCalc.Domain;

namespace LoanCalc.Engine
{
    public interface ILoanEngineConfiguration
    {
        decimal AnnualInterestRatePercentage { get; }

        InterestRateType InterestRateType { get; }

        decimal AdminFeePercentage { get; }

        decimal AdminFeeAmount { get; }
    }
}