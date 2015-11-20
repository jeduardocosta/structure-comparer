using System;
using System.Collections.Generic;
using System.Linq;
using StructureComparer.Models;

namespace StructureComparer.Validators
{
    internal class EnumTypeValidator : IBaseTypeValidator
    {
        private readonly IEnumerable<Func<Type, Type, StructureComparisonResult>> _validations;

        private readonly ITypeValidator _typeValidator;

        internal EnumTypeValidator(ITypeValidator typeValidator)
        {
            _typeValidator = typeValidator;
        }

        public EnumTypeValidator()
            : this(new TypeValidator())
        {
            _validations = new List<Func<Type, Type, StructureComparisonResult>>
            {
                ValidateNames, ValidateValues
            };
        }

        public StructureComparisonResult Validate(Type baseType, Type toCompareType)
        {
            var comparisonResult = new StructureComparisonResult();

            if (_typeValidator.IsNullable(baseType) && _typeValidator.IsNullable(toCompareType))
            {
                baseType = GetEnumTypeFromNullableType(baseType);
                toCompareType = GetEnumTypeFromNullableType(toCompareType);
            }

            foreach (var validation in _validations)
            {
                var validatonResult = validation(baseType, toCompareType);

                if (!validatonResult.AreEqual)
                {
                    comparisonResult.AddError(validatonResult.DifferencesString);
                }
            }

            return comparisonResult;
        }

        private StructureComparisonResult ValidateNames(Type baseType, Type toCompareType)
        {
            var comparisonResult = new StructureComparisonResult();

            var baseTypeNames = GetNames(baseType);
            var toCompareTypeNames = GetNames(toCompareType);

            var areEqual = baseTypeNames.SequenceEqual(toCompareTypeNames);

            if (!areEqual)
            {
                comparisonResult.AddError($"Failed to validate structures. Type 1: '{baseType.Name}', Type 2: '{toCompareType.Name}'. Reason: divergent enum names");
            }

            return comparisonResult;
        }

        private StructureComparisonResult ValidateValues(Type baseType, Type toCompareType)
        {
            var comparisonResult = new StructureComparisonResult();

            var baseTypeValues = GetValues(baseType).ToList();
            var toCompareTypeValues = GetValues(toCompareType).ToList();

            var areEqual = baseTypeValues.SequenceEqual(toCompareTypeValues);

            if (!areEqual)
            {
                comparisonResult.AddError($"failed to validate structures. Type 1: '{baseType.Name}', Type 2: '{toCompareType.Name}'. Reason: divergent enum values");
            }

            return comparisonResult;
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