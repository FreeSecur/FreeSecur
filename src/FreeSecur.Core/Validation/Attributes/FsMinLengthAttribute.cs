using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.Validation.Attributes
{
    public class FsMinLengthAttribute : MinLengthAttribute, IFsValidationAttribute
    {
        public FsMinLengthAttribute(int length) : base(length)
        {
        }

        public FieldValidationErrorCode GetErrorCode()
            => FieldValidationErrorCode.MinLength;
    }
}
