using System.ComponentModel.DataAnnotations;

namespace FreeSecur.Core.Validation.Attributes
{
    public class FsRequiredAttribute : RequiredAttribute, IFsValidationAttribute
    {
        public FieldValidationErrorCode GetErrorCode()
            => FieldValidationErrorCode.Required;
    }
}
