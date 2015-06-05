using System;

namespace ClassPropertyValidator.Validators
{
    internal interface ITypeBaseValidator
    {
        bool Validate(Type baseType, Type toCompareType);
    }
}