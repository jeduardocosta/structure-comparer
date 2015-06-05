using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassPropertyValidator.Validators
{
    internal class ComplexTypeBaseValidator : ITypeBaseValidator
    {
        private readonly IEnumerable<Func<Type, Type, bool>> _validations;

        private readonly ITypeTypeValidator _typeTypeValidator;

        internal ComplexTypeBaseValidator(ITypeTypeValidator typeTypeValidator)
        {
            _typeTypeValidator = typeTypeValidator;
        }

        public ComplexTypeBaseValidator()
            : this(new TypeTypeValidator())
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
            Type baseTypeCollectionType = null;
            Type toCompareBaseCollectionType = null;

            if (BothTypesAreCollections(baseType, toCompareType))
            {
                baseTypeCollectionType = ExtractTypeCollection(baseType).FirstOrDefault();
                toCompareBaseCollectionType = ExtractTypeCollection(baseType).FirstOrDefault();
            }

            return true;
        }

        private bool BothTypesAreCollections(Type baseType, Type toCompareType)
        {
            return _typeTypeValidator.IsEnumerableType(baseType) && _typeTypeValidator.IsEnumerableType(toCompareType);
        }

        private static IEnumerable<Type> ExtractTypeCollection(Type type)
        {
            return type.GetGenericArguments();
        }
    }
}