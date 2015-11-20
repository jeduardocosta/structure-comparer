using System;
using StructureComparer.Models;

namespace StructureComparer.Validators
{
    internal interface ITypeValidator
    {
        bool IsPrimitive(Type type);
        bool IsEnum(Type type);
        bool IsComplexType(Type type);
        bool IsEnumerableType(Type type);
        bool IsNullable(Type type);
        StructureComparisonResult ValidateName(Type baseType, Type toCompareType);
        bool ValidatePropertiesNumber(Type baseType, Type toCompareType);
    }
}