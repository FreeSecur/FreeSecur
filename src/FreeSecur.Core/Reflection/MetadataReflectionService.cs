using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FreeSecur.Core.Reflection
{
    public class MetadataReflectionService
    {
        public MetadataReflectionService()
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
    }
}
