namespace FreeSecur.Core.Validation
{
    public interface IFsValidationAttribute
    {
        FieldValidationErrorCode GetErrorCode();
        bool IsValid(object value);
    }
}
