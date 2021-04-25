using LoanCalc.Domain;
using LoanCalc.WebApi.Models;

namespace LoanCalc.WebApi.Services
{
    public interface ILoanConverter
    {
        PaymentOverviewModel Convert(PaymentOverview entity);
    }
}