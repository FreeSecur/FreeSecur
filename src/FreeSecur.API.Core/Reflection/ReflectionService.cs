using FreeSecur.API.Core.GeneralExtensions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FreeSecur.API.Core.Reflection
{
    public class ReflectionService
    {
        public ReflectionService()
        {
            _reflectionInfoTypesCache = new ConcurrentDictionary<Type, ReflectionInfo>();
            _metadataTypeCache = new ConcurrentDictionary<Type, Type>();
        }

        private static readonly Type _listType = typeof(IEnumerable);
        private readonly ConcurrentDictionary<Type, ReflectionInfo> _reflectionInfoTypesCache;
        private readonly ConcurrentDictionary<Type, Type> _metadataTypeCache;

        public bool IsCollection(Type type) => _listType.IsAssignableFrom(type) && type != typeof(string);
        public Type GetElementType(Type type)
        {
            if (!IsCollection(type))
            {
                throw new ArgumentException("Type must be of type IEnumerable and not be a string");
            }

            return type.IsArray ? type.GetElementType() : type.GenericTypeArguments[0];
        }

        public ReflectionInfo GetReflectionInfo(Type type)
        {
            var reflectionInfo = _reflectionInfoTypesCache.GetOrAdd(type, BuildReflectionInfo);
            return reflectionInfo;
        }

        public Type GetMetadataType(Type classType)
        {
            var metadataType = _metadataTypeCache.GetOrAdd(classType, BuildMetadataType);
            return metadataType;
        }

        /// <summary>
        /// Gets value from model based on a "." seperated key. For example TestModel.TestField
        /// DOES NOT SUPPORT DICTIONARIES
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns>Value of requested key</returns>
        public object GetValueForKey(string key, object model)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var keyElements = key.Split('.');
            var containsEmptyStrings = keyElements.Any(x => string.IsNullOrWhiteSpace(x));

            if (containsEmptyStrings)
            {
                throw new ArgumentException($"{nameof(key)} contains empty elements. value: {key}");
            }

            var modelType = model.GetType();
            var currentModel = model;

            foreach (var keyElement in keyElements)
            {
                try
                {
                    var newModelValue = GetValueForPropertyName(currentModel, keyElement);
                    currentModel = newModelValue;
                }
                catch (Exception e)
                {
                    throw GetMissingKeyException(key, keyElement, modelType, e);
                }
            }

            return currentModel;
        }

        private object GetValueForPropertyName(object currentModel, string keyElement)
        {
            if (currentModel == null) throw new ArgumentException(nameof(currentModel));

            var fullArraySelector = keyElement.GetMatchedValues('[', ']').SingleOrDefault();
            if (fullArraySelector != null)
            {
                var arraySelector = fullArraySelector.Substring(1, fullArraySelector.Length - 2);
                var arrayIndex = int.Parse(arraySelector);
                var propertyName = keyElement.Split('[')[0];

                var resultValue = GetValueForPropertyName(currentModel, propertyName, arrayIndex);
                return resultValue;
            }
            else
            {
                var resultValue = GetValueForPropertyName(currentModel, keyElement, null);
                return resultValue;
            }
        }

        private object GetValueForPropertyName(object currentModel, string propertyName, int? arrayIndex)
        {
            if (currentModel == null) throw new ArgumentException(nameof(currentModel));

            var currentType = currentModel.GetType();
            var metadata = GetReflectionInfo(currentType);
            var propertyInfo = metadata.Properties
                .Where(x => x.Property.Name == propertyName)
                .Select(x => x.Property)
                .SingleOrDefault();

            if (propertyInfo == null)
            {
                throw new ArgumentException(nameof(PropertyInfo));
            }

            var propertyValue = propertyInfo.GetValue(currentModel);

            if (arrayIndex.HasValue)
            {
                var listValue = (IList)propertyValue;
                var elementValue = listValue[arrayIndex.Value];
                return elementValue;
            }
            else
            {
                return propertyValue;
            }
        }

        private ReflectionInfo BuildReflectionInfo(Type type)
        {
            var metaDataType = GetMetadataType(type);
            var customAttributes = type.GetCustomAttributes().ToList();

            var properties = type.GetProperties();

            var reflectionProperties = properties.Select(property =>
            {
                var metaProperty = metaDataType?.GetProperty(property.Name);

                var customAttributes = property.GetCustomAttributes(true).Select(attribute => (Attribute)attribute);

                var combinedAttributes = new List<Attribute>();
                combinedAttributes.AddRange(customAttributes);

                if (metaProperty != null)
                {
                    var customAttributesMeta = metaProperty.GetCustomAttributes(true).Select(attribute => (Attribute)attribute);
                    combinedAttributes.AddRange(customAttributesMeta);
                }


                return new ReflectionPropertyInfo(property, metaProperty, combinedAttributes);
            }).ToList();

            var reflectionInfo = new ReflectionInfo(metaDataType, customAttributes, reflectionProperties);

            return reflectionInfo;
        }

        private Type BuildMetadataType(Type type)
        {
            var metadataType = type
                .GetCustomAttributes(true)
                .Where(x => x is MetadataTypeAttribute)
                .Select(x => x as MetadataTypeAttribute)
                .FirstOrDefault()?.MetadataClassType;

            return metadataType;
        }

        private Exception GetMissingKeyException(string key, string keyElement, Type modelType, Exception e)
        {
            return new ArgumentException($"Requested key does not exist in given model. FullKey: {key} KeyElement: {keyElement} ModelType: {modelType.FullName}", e);
        }
    }
}
