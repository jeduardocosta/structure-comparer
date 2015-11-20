using System;
using StructureComparer.Models;

namespace StructureComparer.Validators.Flows
{
    internal interface IValidationFlow
    {
        StructureComparisonResult Validate(Type baseType, Type toCompareType, string propertyName);
    }
}