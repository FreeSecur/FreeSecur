using FreeSecur.API.Core.Validation.ErrorCodes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.API.Core.Validation.Attributes
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
