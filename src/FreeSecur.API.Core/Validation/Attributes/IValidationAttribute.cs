using FreeSecur.API.Core.Validation.ErrorCodes;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.API.Core.Validation.Attributes
{
    public interface IValidationAttribute
    {
        FieldValidationErrorCode GetErrorCode();
        bool IsValid(object value);
        ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsValid(value))
            {
                return new ValidationResult($"Validation of {GetErrorCode()} failed");
            }

            return null;
        }
    }
}
