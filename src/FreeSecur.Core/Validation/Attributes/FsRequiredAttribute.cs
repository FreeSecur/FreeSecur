using FreeSecur.Core.Validation.ErrorCodes;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.Core.Validation.Attributes
{
    public class FsRequiredAttribute : RequiredAttribute, IValidationAttribute
    {
        public FieldValidationErrorCode GetErrorCode()
            => FieldValidationErrorCode.Required;
    }
}
