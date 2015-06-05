using System;

namespace ClassPropertyValidator.Validators
{
    internal class PrimitiveTypeValidator : IBaseTypeValidator
    {
        public bool Validate(Type baseType, Type toCompareType)
        {
            return baseType == toCompareType;
        }
    }
}