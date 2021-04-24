using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanCalc.WebApi.Models
{
    public class ResultModel<TPayload>
    {
        public string Error { get; private set; }

        public TPayload Payload { get; private set; }

        public IReadOnlyDictionary<string, string> ValidationErrors { get; private set; }

        private ResultModel() {}

        public static ResultModel<TPayload> FromPayload(TPayload payload) =>
            new ResultModel<TPayload>
            {
                Payload = payload
            };

        public static ResultModel<TPayload> FromError(string errorMessage) =>
            new ResultModel<TPayload>
            {
                Error = errorMessage ?? "Unspecified error"
            };

        public static ResultModel<TPayload> FromValidationErrors(IReadOnlyDictionary<string, string> validationErrors) =>
            new ResultModel<TPayload>
            {
                ValidationErrors = validationErrors
            };

        public string ValidationErrorMessage => ValidationErrors?.Aggregate(
            "Validation errors:",
            (result, pair) => $"{result} {pair.Key}:{pair.Value}");
    }
}