using FreeSecur.API.Core.Validation.ErrorCodes;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.API.Core.Validation.Attributes
{
    public class FsRequiredAttribute : RequiredAttribute, IValidationAttribute
    {
        public FieldValidationErrorCode GetErrorCode()
            => FieldValidationErrorCode.Required;
    }
}
