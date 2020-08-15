using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FreeSecur.Core.Validation.Attributes
{
    public class FsUrlAttribute : DataTypeAttribute, IFsValidationAttribute
    {
        private static Regex _regex = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");

        public FsUrlAttribute()
            : base(DataType.Url)
        {
            ErrorMessage = "The {0} field is not a valid url.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string valueAsString = value as string;
            return valueAsString != null && _regex.Match(valueAsString).Length > 0;
        }

        public FieldValidationErrorCode GetErrorCode()
            => FieldValidationErrorCode.Url;
    }
}
