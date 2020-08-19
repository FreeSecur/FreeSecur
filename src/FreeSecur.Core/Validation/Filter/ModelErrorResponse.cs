using FreeSecur.Core.Validation.ErrorCodes;
using System.Collections.Generic;

namespace FreeSecure.Core.Validation.Filter
{
    public class ModelErrorResponse : IErrorResponse<ModelValidationErrorCode>
    {
        public ModelErrorResponse(string message)
        {
            Message = message;
        }

        public ModelErrorResponse(List<ValidationResult> argumentValidationResults)
        {
            ArgumentValidationResults = argumentValidationResults;
        }

        public ModelValidationErrorCode ErrorCode => ModelValidationErrorCode.InvalidModel;
        public List<ValidationResult> ArgumentValidationResults { get; }
        public string Message { get; }
    }
}
