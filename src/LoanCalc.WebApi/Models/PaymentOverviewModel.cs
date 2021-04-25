namespace LoanCalc.WebApi.Models
{
    public class PaymentOverviewModel
    {
        public decimal AnnualInterestRate { get; set; }

        public decimal MonthlyCost { get; set; }

        public decimal TotalInterestRateAmount { get; set; }

        public decimal TotalAdministrativeFees { get; set; }
    }
}