using System;

namespace ClassPropertyValidator.Validators
{
    internal interface IBaseTypeValidator
    {
        bool Validate(Type baseType, Type toCompareType);
    }
}