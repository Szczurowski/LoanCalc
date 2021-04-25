namespace LoanCalc.Domain
{
    // TODO: Consider using struct, after all we have only decimals as of now
    public class PaymentOverview
    {
        public decimal ActualAnnualInterestRate { get; set; }

        public decimal MonthlyCost { get; set; }

        public decimal TotalInterest { get; set; }

        public decimal TotalAdminFees { get; set; }
    }
}
