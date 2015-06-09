using System;
using System.Collections.Generic;
using System.Linq;

namespace StructureComparer.Validators
{
    internal class ComplexTypeValidator : IBaseTypeValidator
    {
        private readonly IEnumerable<Func<Type, Type, bool>> _validations;
        
        private readonly ITypeValidator _typeValidator;
        private readonly IStructureComparer _structureComparer;

        internal ComplexTypeValidator(ITypeValidator typeTypeValidator, 
            IStructureComparer structureComparer)
        {
            _typeValidator = typeTypeValidator;
            _structureComparer = structureComparer;
        }

        public ComplexTypeValidator()
            : this(new TypeValidator(), new StructureComparer())
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

            var validationResult = _structureComparer.Compare(baseComplexType, toCompareComplexType);
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