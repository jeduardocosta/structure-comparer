using System;
using System.Collections.Generic;
using StructureComparer.Extensions;
using StructureComparer.Models;

namespace StructureComparer.Validators.Flows
{
    internal class ValidationFlow : IValidationFlow
    {
        private readonly ITypeValidator _typeValidator;
        private readonly IBaseTypeValidator _baseTypeValidator;

        private IEnumerable<Func<Type, Type, StructureComparisonResult>> _validations;

        public ValidationFlow(ITypeValidator typeValidator, IBaseTypeValidator baseTypeValidator)
        {
            _typeValidator = typeValidator;
            _baseTypeValidator = baseTypeValidator;
        }

        public StructureComparisonResult Validate(Type baseType, Type toCompareType, string propertyName)
        {
            _validations = new List<Func<Type, Type, StructureComparisonResult>>
            {
                _typeValidator.ValidateName,
                _baseTypeValidator.Validate
            };

            var comparisonResult = new StructureComparisonResult();

            foreach (var validation in _validations)
            {
                var validatonResult = validation(baseType, toCompareType);

                if (!validatonResult.AreEqual)
                {
                    comparisonResult.AddError(validatonResult.DifferencesString.AppendPropertyName(propertyName));
                }
            }

            return comparisonResult;
        }
    }
}