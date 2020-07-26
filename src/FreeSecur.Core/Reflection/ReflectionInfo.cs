using System;
using System.Collections.Generic;

namespace FreeSecur.Core.Reflection
{
    public class ReflectionInfo
    {
        public ReflectionInfo(Type metaDataType,
            List<Attribute> customAttributes,
            List<ReflectionPropertyInfo> properties)
        {
            MetaDataType = metaDataType;
            CustomAttributes = customAttributes;
            Properties = properties;
        }

        public Type MetaDataType { get; }
        public List<Attribute> CustomAttributes { get; }
        public List<ReflectionPropertyInfo> Properties { get; }
    }
}
