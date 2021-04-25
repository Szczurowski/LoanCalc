namespace LoanCalc.Domain
{
    public class PaymentOverview
    {
        public decimal ActualAnnualInterestRate { get; set; }

        public decimal MonthlyCost { get; set; }

        public decimal TotalInterest { get; set; }

        public decimal TotalAdminFees { get; set; }
    }
}
