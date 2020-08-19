using FreeSecur.API.Core.Reflection;
using FreeSecur.API.Core.Validation.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FreeSecur.API.Core.Validation.Validator
{
    public class ModelValidator
    {
        private readonly ReflectionService _reflectionService;

        public ModelValidator(ReflectionService reflectionService)
        {
            _reflectionService = reflectionService;
        }

        public List<FieldValidationResult> Validate(object model)
        {
            var mainType = model.GetType();
            var validationResults = new List<FieldValidationResult>();

            var isCollection = _reflectionService.IsCollection(mainType);
            if (isCollection)
            {
                var index = 0;
                foreach (var element in (IEnumerable)model)
                {
                    var singularObjectValidationResults = ValidateSingularObject(element, index);
                    validationResults.AddRange(singularObjectValidationResults);
                    index++;
                }

            }
            else
            {
                var singularObjectValidationResults = ValidateSingularObject(model);
                validationResults.AddRange(singularObjectValidationResults);
            }

            return validationResults;
        }

        private List<FieldValidationResult> ValidateSingularObject(object model)
        {
            return ValidateSingularObject(model, null);
        }

        private List<FieldValidationResult> ValidateSingularObject(object model, int? index)
        {
            var mainType = model.GetType();

            var reflectionInfo = _reflectionService.GetReflectionInfo(mainType);
            var validationResults = reflectionInfo.Properties
                .Select(property => ValidateProperty(property, model, index))
                .Where(validationResult => !validationResult.IsValid)
                .ToList();

            return validationResults;
        }

        private FieldValidationResult ValidateProperty(
            ReflectionPropertyInfo reflectionPropertyInfo, 
            object mainObject, 
            int? index)
        {
            var propertyInfo = reflectionPropertyInfo.Property;
            var propertyType = reflectionPropertyInfo.Property.PropertyType;
            var propertyValue = propertyInfo.GetValue(mainObject);

            var validationAttributes = reflectionPropertyInfo.CustomAttributes
                .Where(attribute => attribute is IValidationAttribute)
                .Select(attribute => (IValidationAttribute)attribute)
                .ToList();

            var errorCodes = validationAttributes
                .Where(validationAttrbute => !validationAttrbute.IsValid(propertyValue))
                .Select(validationAttribute => validationAttribute.GetErrorCode())
                .ToList();


            var subFieldValidationResults = new List<FieldValidationResult>();
            if (propertyValue != null)
            {
                var metadataType = _reflectionService.GetMetadataType(propertyType);
                if (metadataType != null)
                {
                    var propertyValidationResults  = ValidateSingularObject(propertyValue);
                    subFieldValidationResults.AddRange(subFieldValidationResults);

                }
                else if (_reflectionService.IsCollection(propertyType))
                {
                    var elementList = (IEnumerable)propertyValue;
                    var subIndex = 0;
                    foreach(var element in elementList)
                    {
                        var elementValidationResults = ValidateSingularObject(element, subIndex);
                        subFieldValidationResults.AddRange(elementValidationResults);
                        subIndex++;
                    }
                }
            }

            if (subFieldValidationResults.Any())
            {
                return new FieldValidationResult(propertyInfo.Name, errorCodes, subFieldValidationResults, index);
            }
            else
            {
                return new FieldValidationResult(propertyInfo.Name, errorCodes, index);
            }
        }
    }
}
