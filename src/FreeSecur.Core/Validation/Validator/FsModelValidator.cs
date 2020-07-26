using FreeSecur.Core.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreeSecur.Core.Validation.Validator
{
    public class FsModelValidator
    {
        private readonly MetadataReflectionService _metadataReflectionService;

        public FsModelValidator(MetadataReflectionService metadataReflectionService)
        {
            _metadataReflectionService = metadataReflectionService;
        }

        public FsModelValidationResult Validate(object model)
        {
            var mainType = model.GetType();
            var validationResults = new List<FsFieldValidationResult>();

            var isCollection = _metadataReflectionService.IsCollection(mainType);
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

            return new FsModelValidationResult(validationResults);
        }

        private List<FsFieldValidationResult> ValidateSingularObject(object model)
        {
            return ValidateSingularObject(model, null);
        }

        private List<FsFieldValidationResult> ValidateSingularObject(object model, int? index)
        {
            var mainType = model.GetType();

            var reflectionInfo = _metadataReflectionService.GetReflectionInfo(mainType);
            var validationResults = reflectionInfo.Properties
                .Select(property => ValidateProperty(property, model, index))
                .ToList();

            return validationResults;
        }

        private FsFieldValidationResult ValidateProperty(
            ReflectionPropertyInfo reflectionPropertyInfo, 
            object mainObject, 
            int? index)
        {
            var propertyInfo = reflectionPropertyInfo.Property;
            var propertyType = reflectionPropertyInfo.Property.PropertyType;
            var propertyValue = propertyInfo.GetValue(mainObject);

            var validationAttributes = reflectionPropertyInfo.CustomAttributes
                .Where(attribute => attribute is IFsValidationAttribute)
                .Select(attribute => (IFsValidationAttribute)attribute)
                .ToList();

            var errorCodes = validationAttributes
                .Where(validationAttrbute => !validationAttrbute.IsValid(propertyValue))
                .Select(validationAttribute => validationAttribute.GetErrorCode())
                .ToList();


            var subFieldValidationResults = new List<FsFieldValidationResult>();
            if (propertyValue != null)
            {
                var metadataType = _metadataReflectionService.GetMetadataType(propertyType);
                if (metadataType != null)
                {
                    var propertyValidationResults  = ValidateSingularObject(propertyValue);
                    subFieldValidationResults.AddRange(subFieldValidationResults);

                }
                else if (_metadataReflectionService.IsCollection(propertyType))
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
                return new FsFieldValidationResult(propertyInfo.Name, errorCodes, subFieldValidationResults, index);
            }
            else
            {
                return new FsFieldValidationResult(propertyInfo.Name, errorCodes, index);
            }
        }
    }
}
