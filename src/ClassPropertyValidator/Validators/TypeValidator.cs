using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassPropertyValidator.Validators
{
    internal interface ITypeValidator
    {
        bool IsPrimitive(Type type);
        bool IsEnum(Type type);
        bool IsComplexType(Type type);
        bool IsEnumerableType(Type type);

        bool ValidateName(Type baseType, Type toCompareType);
        bool ValidatePropertiesNumber(Type baseType, Type toCompareType);
        bool ValidateSameType(Type baseType, Type toCompareType);
    }

    internal class TypeValidator : ITypeValidator
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

        public bool ValidateName(Type baseType, Type toCompareType)
        {
            var baseTypeName = baseType.Name;
            var toCompareTypeName = toCompareType.Name;

            return baseTypeName == toCompareTypeName;
        }

        public bool ValidatePropertiesNumber(Type baseType, Type toCompareType)
        {
            var baseTypePropertiesNumber = baseType.GetProperties().Count();
            var toCompareTypePropertiesNumber = toCompareType.GetProperties().Count();

            return baseTypePropertiesNumber == toCompareTypePropertiesNumber;
        }

        public bool ValidateSameType(Type baseType, Type toCompareType)
        {
            return true;
        }

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