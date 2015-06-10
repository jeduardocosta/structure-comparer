using System;
using StructureComparer.Models;

namespace StructureComparer.Validators
{
    internal interface IBaseTypeValidator
    {
        StructureComparisonResult Validate(Type baseType, Type toCompareType);
    }
}