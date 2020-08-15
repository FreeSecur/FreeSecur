using FreeSecur.Core.Validation;
using System.Collections.Generic;

namespace FreeSecure.API.ErrorHandling
{
    public class FsModelErrorResponse : IFsErrorResponse<ModelValidationErrorCode>
    {
        public FsModelErrorResponse(string message)
        {
            Message = message;
        }

        public FsModelErrorResponse(List<FsValidationResult> argumentValidationResults)
        {
            ArgumentValidationResults = argumentValidationResults;
        }

        public ModelValidationErrorCode ErrorCode => ModelValidationErrorCode.InvalidModel;
        public List<FsValidationResult> ArgumentValidationResults { get; }
        public string Message { get; }
    }
}
