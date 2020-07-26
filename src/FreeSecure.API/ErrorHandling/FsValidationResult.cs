using FreeSecur.Core.Validation.Validator;
using System.Collections.Generic;

namespace FreeSecure.API.ErrorHandling
{
    public class FsValidationResult
    {
        public FsValidationResult(string argumentName, List<FsFieldValidationResult> validationResults)
        {
            ArgumentName = argumentName;
            ValidationResults = validationResults;
        }

        public string ArgumentName { get; }
        public List<FsFieldValidationResult> ValidationResults { get; }
    }
}
