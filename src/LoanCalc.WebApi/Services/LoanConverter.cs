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
                    ActualAnnualInterestRate = entity.ActualAnnualInterestRate,
                    MonthlyCost = entity.MonthlyCost,
                    TotalAdminFees = entity.TotalAdminFees,
                    TotalInterest = entity.TotalInterest
                }
                : null;
    }
}