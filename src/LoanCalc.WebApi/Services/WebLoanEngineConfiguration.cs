using System;
using LoanCalc.Domain;
using LoanCalc.Engine;
using Microsoft.Extensions.Configuration;

namespace LoanCalc.WebApi.Services
{
    public class WebLoanEngineConfiguration : ILoanEngineConfiguration
    {
        private readonly IConfiguration _configuration;

        public WebLoanEngineConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public decimal AnnualInterestRatePercentage => GetEngineValue<decimal>("AnnualInterestRatePercentage");

        public InterestRateType InterestRateType => 
            Enum.Parse<InterestRateType>(GetEngineValue<string>("InterestRateType"), true);

        public decimal AdminFeePercentage => GetEngineValue<decimal>("AdminFeePercentage");

        public decimal AdminFeeAmount => GetEngineValue<decimal>("AdminFeeAmount");

        private T GetEngineValue<T>(string key) => _configuration.GetValue<T>($"Engine:{key}");
    }
}