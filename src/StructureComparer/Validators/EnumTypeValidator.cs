using System;
using System.Collections.Generic;
using System.Linq;

namespace StructureComparer.Validators
{
    internal class EnumTypeValidator : IBaseTypeValidator
    {
        private readonly IEnumerable<Func<Type, Type, bool>> _validations;

        private readonly ITypeValidator _typeValidator;

        internal EnumTypeValidator(ITypeValidator typeValidator)
        {
            _typeValidator = typeValidator;
        }

        public EnumTypeValidator()
            : this(new TypeValidator())
        {
            _validations = new List<Func<Type, Type, bool>>
            {
                ValidateNames, ValidateValues
            };
        }

        public bool Validate(Type baseType, Type toCompareType)
        {
            if (_typeValidator.IsNullable(baseType) && _typeValidator.IsNullable(toCompareType))
            {
                baseType = GetEnumTypeFromNullableType(baseType);
                toCompareType = GetEnumTypeFromNullableType(toCompareType);
            }

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

        private static Type GetEnumTypeFromNullableType(Type @type)
        {
            return Nullable.GetUnderlyingType(type);
        }
    }
}