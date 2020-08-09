using System;
using System.Collections.Generic;
using System.Reflection;

namespace FreeSecur.Core.Reflection
{
    public class ReflectionPropertyInfo
    {
        public ReflectionPropertyInfo(PropertyInfo property,
            PropertyInfo metaPropery,
            List<Attribute> customAttributes)
        {
            Property = property;
            MetaPropery = metaPropery;
            CustomAttributes = customAttributes;
        }

        public PropertyInfo Property { get; }
        public PropertyInfo MetaPropery { get; }
        public List<Attribute> CustomAttributes { get; }
    }
}
