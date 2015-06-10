using System;
using StructureComparer.Models;

namespace StructureComparer.Validators
{
    internal class PrimitiveTypeValidator : IBaseTypeValidator
    {
        public StructureComparisonResult Validate(Type baseType, Type toCompareType)
        {
            return new StructureComparisonResult();
        }
    }
}