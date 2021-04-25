using LoanCalc.Domain;

namespace LoanCalc.Engine
{
    public interface ICalculationEngine
    {
        PaymentOverview GeneratePaymentOverview(decimal amount, int duration);
    }
}