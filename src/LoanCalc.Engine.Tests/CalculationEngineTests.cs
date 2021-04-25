using LoanCalc.Domain;
using Moq;
using NUnit.Framework;

namespace LoanCalc.Engine.Tests
{
    [TestFixture]
    public class CalculationEngineTests
    {
        private CalculationEngine _sut;

        [SetUp]
        public void Setup()
        {
            var configMock = new Mock<ILoanEngineConfiguration>();
            configMock.SetupGet(x => x.AdminFeeAmount).Returns(10_000M);
            configMock.SetupGet(x => x.AdminFeePercentage).Returns(1M);
            configMock.SetupGet(x => x.InterestRateType).Returns(InterestRateType.Monthly);
            configMock.SetupGet(x => x.AnnualInterestRatePercentage).Returns(5M);

            _sut = new CalculationEngine(configMock.Object);
        }

        [Test]
        public void GeneratePaymentOverview_WhenExampleInputProvided_ShouldMonthlyCostPassAssertion()
        {
            // Assign, Act
            var paymentOverview = _sut.GeneratePaymentOverview(500_000M, 120);

            // Assert
            Assert.IsNotNull(paymentOverview);
            Assert.AreEqual(paymentOverview.MonthlyCost, 5303.28M);
        }

        [Test]
        public void GeneratePaymentOverview_WhenExampleInputProvided_ShouldTotalInterestPassAssertion()
        {
            // Assign, Act
            var paymentOverview = _sut.GeneratePaymentOverview(500_000M, 120);

            // Assert
            Assert.IsNotNull(paymentOverview);
            Assert.AreEqual(paymentOverview.TotalInterest, 136_393.6M);
        }

        [Test]
        public void GeneratePaymentOverview_WhenExampleInputProvided_ShouldTotalAdminFeesPassAssertion()
        {
            // Assign, Act
            var paymentOverview = _sut.GeneratePaymentOverview(500_000M, 120);

            // Assert
            Assert.IsNotNull(paymentOverview);
            Assert.AreEqual(paymentOverview.TotalAdminFees, 5_000M);
        }
    }
}