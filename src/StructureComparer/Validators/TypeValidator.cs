﻿using System;
using System.Collections.Generic;
using System.Linq;
using StructureComparer.Extensions;
using StructureComparer.Models;

namespace StructureComparer.Validators
{
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

        public StructureComparisonResult ValidateName(Type baseType, Type toCompareType)
        {
            var comparisonResult = new StructureComparisonResult();

            string baseTypeName;
            string toCompareTypeName;
            
            baseType = GetCurrentTypeOrBaseTypeFromNullableType(baseType);
            toCompareType = GetCurrentTypeOrBaseTypeFromNullableType(toCompareType);

            if (IsEnumerableType(baseType) && IsEnumerableType(toCompareType))
            {
                baseTypeName = baseType.GenericTypeArguments.Select(c => c.Name).FirstOrDefault();
                toCompareTypeName = toCompareType.GenericTypeArguments.Select(c => c.Name).FirstOrDefault();
            }
            else
            {
                baseTypeName = baseType.Name;
                toCompareTypeName = toCompareType.Name;
            }

            var areEqual = baseTypeName == toCompareTypeName;

            if (!areEqual)
            {
                comparisonResult.AddError(baseType, toCompareType);
            }

            return comparisonResult;
        }

        public bool ValidatePropertiesNumber(Type baseType, Type toCompareType)
        {
            var baseTypePropertiesNumber = baseType.GetProperties().Count();
            var toCompareTypePropertiesNumber = toCompareType.GetProperties().Count();

            return baseTypePropertiesNumber == toCompareTypePropertiesNumber;
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

        public bool IsNullable(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        private static bool IsString(Type type)
        {
            return type == typeof(string) || type == typeof(String);
        }

        private Type GetCurrentTypeOrBaseTypeFromNullableType(Type type)
        {
            if (IsNullable(type))
            {
                return type.GetBaseTypeFromTypeNullable();
            }

            return type;
        }
    }
}