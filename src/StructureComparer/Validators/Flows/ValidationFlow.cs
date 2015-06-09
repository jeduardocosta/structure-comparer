using System;
using System.Collections.Generic;
using System.Linq;

namespace StructureComparer.Validators.Flows
{
    internal interface IValidationFlow
    {
        bool Validate(Type baseType, Type toCompareType);
    }

    internal class ValidationFlow : IValidationFlow
    {
        private readonly ITypeValidator _typeValidator;
        private readonly IBaseTypeValidator _baseTypeValidator;

        private IEnumerable<Func<Type, Type, bool>> _validations;

        public ValidationFlow(ITypeValidator typeValidator, IBaseTypeValidator baseTypeValidator)
        {
            _typeValidator = typeValidator;
            _baseTypeValidator = baseTypeValidator;
        }

        public bool Validate(Type baseType, Type toCompareType)
        {
            _validations = new List<Func<Type, Type, bool>>
            {
                _typeValidator.ValidateName,
                _baseTypeValidator.Validate
            };

            return _validations
                .ToList()
                .All(validation => validation(baseType, toCompareType));
        }
    }
}