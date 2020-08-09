using System;
using System.ComponentModel.DataAnnotations;

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
