using LoanCalc.Domain;
using LoanCalc.WebApi.Models;

namespace LoanCalc.WebApi.Services
{
    public class LoanConverter : ILoanConverter
    {
        public PaymentOverviewModel Convert(PaymentOverview entity) =>
            entity != null
                ? new PaymentOverviewModel
                {
                    AnnualInterestRate = entity.AnnualInterestRate,
                    MonthlyCost = entity.MonthlyCost,
                    TotalAdministrativeFees = entity.TotalAdministrativeFees,
                    TotalInterest = entity.TotalInterest
                }
                : null;
    }
}