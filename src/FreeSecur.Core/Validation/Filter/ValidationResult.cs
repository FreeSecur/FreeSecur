using FreeSecur.Core.Validation.Validator;
using System.Collections.Generic;

namespace FreeSecure.Core.Validation.Filter
{
    public class ValidationResult
    {
        public ValidationResult(string argumentName, List<FieldValidationResult> validationResults)
        {
            ArgumentName = argumentName;
            ValidationResults = validationResults;
        }

        public string ArgumentName { get; }
        public List<FieldValidationResult> ValidationResults { get; }
    }
}
