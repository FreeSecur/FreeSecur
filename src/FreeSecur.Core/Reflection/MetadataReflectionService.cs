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
            _implementedTypesCache = new ConcurrentDictionary<Type, List<Type>>();
            _reflectionInfoTypesCache = new ConcurrentDictionary<Type, ReflectionInfo>();
            _metadataTypeCache = new ConcurrentDictionary<Type, Type>();
        }

        private static readonly Type _listType = typeof(IEnumerable);
        private readonly ConcurrentDictionary<Type, List<Type>> _implementedTypesCache;
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

        public List<Type> LoadTypesThatImplement(Type baseType)
        {
            var typesWithBaseType = _implementedTypesCache.GetOrAdd(baseType, BuildTypesThatImplement);
            return typesWithBaseType;
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

        private List<Type> BuildTypesThatImplement(Type baseType)
        {
            var assemblies = new List<Assembly>();
            var entryAssembly = Assembly.GetEntryAssembly();
            assemblies.Add(entryAssembly);

            foreach (var referenceAssembly in entryAssembly.GetReferencedAssemblies())
            {
                assemblies.Add(Assembly.Load(referenceAssembly));
            }

            var typesWithBaseType = new List<Type>();
            foreach (var assembly in assemblies)
            {

                IEnumerable<Type> types;
                try
                {
                    types = assembly.GetTypes();

                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types.Where(x => x != null);
                }

                var loadedTypes = from type in types
                                  where baseType.IsAssignableFrom(type) &&
                                      !type.IsInterface &&
                                      !type.IsAbstract
                                  select type;

                typesWithBaseType.AddRange(loadedTypes);
            };

            return typesWithBaseType;
        }

        
    }
}
