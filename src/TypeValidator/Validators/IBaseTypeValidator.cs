using System;

namespace TypeValidator.Validators
{
    internal interface IBaseTypeValidator
    {
        bool Validate(Type baseType, Type toCompareType);
    }
}