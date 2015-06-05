using System;

namespace ClassPropertyValidator.Validators
{
    internal class PrimitiveTypeBaseValidator : ITypeBaseValidator
    {
        public bool Validate(Type baseType, Type toCompareType)
        {
            return baseType == toCompareType;
        }
    }
}