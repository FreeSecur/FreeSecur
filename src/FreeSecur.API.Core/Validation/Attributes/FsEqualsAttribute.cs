using FreeSecur.API.Core.Validation.ErrorCodes;
using System.ComponentModel.DataAnnotations;

namespace FreeSecur.API.Core.Validation.Attributes
{
    public class FsEqualsAttribute : ValidationAttribute, IValidationAttribute
    {
        private readonly string _propertyToCompare;

        public FsEqualsAttribute(string propertyToCompare)
        {
            _propertyToCompare = propertyToCompare;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var parentObject = validationContext.ObjectInstance;
            var parentObjectType = parentObject.GetType();
            var propertyToCompareWith = parentObjectType.GetProperty(_propertyToCompare);

            var valueToCompare = propertyToCompareWith.GetValue(parentObject);

            if (!valueToCompare.Equals(value))
            {
                return new ValidationResult("Value is not equal to target value");
            }

            return null;
        }

        public FieldValidationErrorCode GetErrorCode() => FieldValidationErrorCode.Equals;

        ValidationResult IValidationAttribute.IsValid(object value, ValidationContext validationContext)
        {
            return IsValid(value, validationContext);
        }
    }
}
