using FreeSecur.API.Core.Validation.ErrorCodes;

namespace FreeSecur.API.Core.Validation.Attributes
{
    public interface IValidationAttribute
    {
        FieldValidationErrorCode GetErrorCode();
        bool IsValid(object value);
    }
}
