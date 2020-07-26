using FreeSecur.Core.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.Validation.Attributes
{
    public class FsRequiredAttribute : RequiredAttribute, IFsValidationAttribute
    {
        public FieldValidationErrorCode GetErrorCode()
            => FieldValidationErrorCode.Required;

    }
}
