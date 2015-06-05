using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassPropertyValidator.Validators.Flows
{
    internal interface IValidationFlow
    {
        bool Validate(Type baseType, Type toCompareType);
    }

    internal class ValidationFlow : IValidationFlow
    {
        private readonly ITypeValidator _typeValidator;
        private readonly ITypeBaseValidator _typeBaseValidator;

        private IEnumerable<Func<Type, Type, bool>> _validations;

        public ValidationFlow(ITypeValidator typeValidator, ITypeBaseValidator typeBaseValidator)
        {
            _typeValidator = typeValidator;
            _typeBaseValidator = typeBaseValidator;
        }

        public bool Validate(Type baseType, Type toCompareType)
        {
            _validations = new List<Func<Type, Type, bool>>
            {
                _typeValidator.ValidateName,
                _typeBaseValidator.Validate
            };

            return _validations
                .ToList()
                .All(validation => validation(baseType, toCompareType));
        }
    }
}