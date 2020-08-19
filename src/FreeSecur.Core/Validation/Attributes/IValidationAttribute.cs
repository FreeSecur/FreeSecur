using FreeSecur.Core.Validation.ErrorCodes;

namespace FreeSecur.Core.Validation.Attributes
{
    public interface IValidationAttribute
    {
        FieldValidationErrorCode GetErrorCode();
        bool IsValid(object value);
    }
}
