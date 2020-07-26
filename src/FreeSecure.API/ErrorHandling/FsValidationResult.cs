using FreeSecur.Core.Validation.Validator;

namespace FreeSecure.API.ErrorHandling
{
    public class FsValidationResult
    {
        public FsValidationResult(string argumentName, FsModelValidationResult validationResult)
        {
            ArgumentName = argumentName;
            ValidationResult = validationResult;
        }

        public string ArgumentName { get; }
        public FsModelValidationResult ValidationResult { get; }
    }
}
