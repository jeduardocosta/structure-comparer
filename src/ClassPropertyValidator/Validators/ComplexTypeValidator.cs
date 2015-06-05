using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassPropertyValidator.Validators
{
    internal class ComplexTypeValidator : IBaseTypeValidator
    {
        private readonly IEnumerable<Func<Type, Type, bool>> _validations;
        
        private readonly ITypeValidator _typeValidator;
        private readonly IClassPropertyValidator _classPropertyValidator;

        internal ComplexTypeValidator(ITypeValidator typeTypeValidator, 
            IClassPropertyValidator classPropertyValidator)
        {
            _typeValidator = typeTypeValidator;
            _classPropertyValidator = classPropertyValidator;
        }

        public ComplexTypeValidator()
            : this(new TypeValidator(), new ClassPropertyValidator())
        {
            _validations = new List<Func<Type, Type, bool>>
            {
                ValidateComplexType
            };
        }

        public bool Validate(Type baseType, Type toCompareType)
        {
            return _validations
                .ToList()
                .All(validation => validation(baseType, toCompareType));
        }

        private bool ValidateComplexType(Type baseType, Type toCompareType)
        {
            var baseComplexType = baseType;
            var toCompareComplexType = toCompareType;

            if (BothTypesAreCollections(baseType, toCompareType))
            {
                baseComplexType = ExtractTypeCollection(baseType).FirstOrDefault();
                toCompareComplexType = ExtractTypeCollection(toCompareType).FirstOrDefault();
            }

            var validationResult = _classPropertyValidator.Validate(baseComplexType, toCompareComplexType);
            return validationResult.AreEqual;
        }

        private bool BothTypesAreCollections(Type baseType, Type toCompareType)
        {
            return _typeValidator.IsEnumerableType(baseType) && 
                   _typeValidator.IsEnumerableType(toCompareType);
        }

        private static IEnumerable<Type> ExtractTypeCollection(Type type)
        {
            return type.GetGenericArguments();
        }
    }
}