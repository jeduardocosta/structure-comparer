using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassPropertyValidator.Validators
{
    internal interface ITypeTypeValidator
    {
        bool IsPrimitive(Type type);
        bool IsEnum(Type type);
        bool IsComplexType(Type type);
        bool IsEnumerableType(Type type);
    }

    internal class TypeTypeValidator : ITypeTypeValidator
    {
        private readonly IEnumerable<Type> _additionalPrimitiveTypes = new[]
        {
            typeof (String),
            typeof (Char),
            typeof (Guid),
            typeof (Boolean),
            typeof (Byte),
            typeof (Int16),
            typeof (Int32),
            typeof (Int64),
            typeof (Single),
            typeof (Double),
            typeof (Decimal),
            typeof (SByte),
            typeof (UInt16),
            typeof (UInt32),
            typeof (UInt64),
            typeof (DateTime),
            typeof (DateTimeOffset),
            typeof (TimeSpan)
        };

        public bool IsPrimitive(Type type)
        {
            return type.IsPrimitive || _additionalPrimitiveTypes.Contains(type) || IsNullable(type);
        }

        public bool IsEnum(Type type)
        {
            return type.IsEnum;
        }

        public bool IsComplexType(Type type)
        {
            return !IsPrimitive(type) && !IsEnum(type);
        }

        public bool IsEnumerableType(Type type)
        {
            return (type.GetInterface("IEnumerable") != null) && !IsString(type);
        }

        private static bool IsString(Type type)
        {
            return type == typeof(string) || type == typeof(String);
        }

        private static bool IsNullable(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}