using FreeSecur.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeSecure.API.ErrorHandling
{
    public class FsModelErrorResponse : IFsErrorResponse<ModelValidationErrorCode>
    {
        public FsModelErrorResponse(List<FsValidationResult> argumentValidationResults)
        {
            ArgumentValidationResults = argumentValidationResults;
        }

        public ModelValidationErrorCode ErrorCode => ModelValidationErrorCode.InvalidModel;
        public List<FsValidationResult> ArgumentValidationResults { get; }
    }
}
