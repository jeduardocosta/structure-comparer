using System;
using System.Linq;

namespace ClassPropertyValidator.Validators
{
    internal interface ITypeValidator
    {
        bool ValidateName(Type baseType, Type toCompareType);
        bool ValidatePropertiesNumber(Type baseType, Type toCompareType);
        bool ValidateSameType(Type baseType, Type toCompareType);
    }

    internal class TypeValidator : ITypeValidator
    {
        public bool ValidateName(Type baseType, Type toCompareType)
        {
            var baseTypeName = baseType.Name;
            var toCompareTypeName = toCompareType.Name;

            return baseTypeName == toCompareTypeName;
        }

        public bool ValidatePropertiesNumber(Type baseType, Type toCompareType)
        {
            var baseTypePropertiesNumber = baseType.GetProperties().Count();
            var toCompareTypePropertiesNumber = toCompareType.GetProperties().Count();

            return baseTypePropertiesNumber == toCompareTypePropertiesNumber;
        }

        public bool ValidateSameType(Type baseType, Type toCompareType)
        {
            return true;
        }
    }
}