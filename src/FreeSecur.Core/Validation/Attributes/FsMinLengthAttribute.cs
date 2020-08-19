using FreeSecur.Core.Validation.ErrorCodes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.Core.Validation.Attributes
{
    public class FsMinLengthAttribute : MinLengthAttribute, IValidationAttribute
    {
        public FsMinLengthAttribute(int length) : base(length)
        {
        }

        public FieldValidationErrorCode GetErrorCode()
            => FieldValidationErrorCode.MinLength;
    }
}
