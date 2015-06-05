using ClassPropertyValidator.Models;
using System;

namespace ClassPropertyValidator.Validators
{
    public interface IClassPropertyValidator
    {
        ClassPropertiesValidationResult Validate(Type baseType, Type toCompareType);
    }

    public class ClassPropertyValidator : IClassPropertyValidator
    {
        private readonly ITypeValidator _typeValidator;

        internal ClassPropertyValidator(ITypeValidator typeValidator)
        {
            _typeValidator = typeValidator;
        }
        
        public ClassPropertyValidator()
            : this(new TypeValidator())
        {
            
        }

        public ClassPropertiesValidationResult Validate(Type baseType, Type toCompareType)
        {
            if (!_typeValidator.ValidatePropertiesNumber(baseType, toCompareType))
                return CreateUnsuccessfulResult("");

            return CreateSuccessfulResult();
        }

        private ClassPropertiesValidationResult CreateSuccessfulResult()
        {
            return new ClassPropertiesValidationResult().CreateSuccessfulResult();
        }

        private ClassPropertiesValidationResult CreateUnsuccessfulResult(string message)
        {
            return new ClassPropertiesValidationResult().CreateUnsuccessfulResult(message);
        }
    }
}