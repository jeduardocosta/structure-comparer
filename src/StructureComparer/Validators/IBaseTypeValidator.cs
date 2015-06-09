using System;

namespace StructureComparer.Validators
{
    internal interface IBaseTypeValidator
    {
        bool Validate(Type baseType, Type toCompareType);
    }
}