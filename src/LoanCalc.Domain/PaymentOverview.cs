namespace LoanCalc.Domain
{
    public class PaymentOverview
    {
        public decimal AnnualInterestRate { get; set; }

        public decimal MonthlyCost { get; set; }

        public decimal TotalInterest { get; set; }

        public decimal TotalAdministrativeFees { get; set; }
    }
}
