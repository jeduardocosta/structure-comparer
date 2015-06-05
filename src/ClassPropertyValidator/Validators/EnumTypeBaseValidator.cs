using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassPropertyValidator.Validators
{
    internal class EnumTypeBaseValidator : ITypeBaseValidator
    {
        private readonly IEnumerable<Func<Type, Type, bool>> _validations;

        public EnumTypeBaseValidator()
        {
            _validations = new List<Func<Type, Type, bool>>
            {
                ValidateNames, ValidateValues
            };
        }

        public bool Validate(Type baseType, Type toCompareType)
        {
            return _validations
                .ToList()
                .All(validation => validation(baseType, toCompareType));
        }

        private bool ValidateNames(Type baseType, Type toCompareType)
        {
            var baseTypeNames = GetNames(baseType);
            var toCompareTypeNames = GetNames(toCompareType);

            return baseTypeNames.SequenceEqual(toCompareTypeNames);
        }

        private bool ValidateValues(Type baseType, Type toCompareType)
        {
            var baseTypeValues = GetValues(baseType).ToList();
            var toCompareTypeValues = GetValues(toCompareType).ToList();

            return baseTypeValues.SequenceEqual(toCompareTypeValues);
        }

        private static IEnumerable<string> GetNames(Type type)
        {
            return Enum.GetNames(type);
        }

        private static IEnumerable<int> GetValues(Type type)
        {
            return Enum.GetValues(type) as IEnumerable<int> ?? new List<int>();
        }
    }
}