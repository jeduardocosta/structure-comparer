using ClassPropertyValidator.Extensions;
using ClassPropertyValidator.Models;
using ClassPropertyValidator.Validators.Flows.Factories;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ClassPropertyValidator.Validators
{
    public interface IClassPropertyValidator
    {
        ClassPropertiesValidationResult Validate(Type baseType, Type toCompareType);
    }

    public class ClassPropertyValidator : IClassPropertyValidator
    {
        private readonly ITypeValidator _typeValidator;
        private readonly IBaseTypeValidator _enumTypeValidator;
        private readonly IPropertyValidator _propertyValidator;
        private readonly IValidationFlowFactory _validationFlowFactory;

        internal ClassPropertyValidator(ITypeValidator typeValidator, 
            IBaseTypeValidator enumTypeValidator,
            IPropertyValidator propertyValidator,
            IValidationFlowFactory validationFlowFactory)
        {
            _typeValidator = typeValidator;
            _enumTypeValidator = enumTypeValidator;
            _propertyValidator = propertyValidator;
            _validationFlowFactory = validationFlowFactory;
        }
        
        public ClassPropertyValidator()
            : this(new TypeValidator(), 
                   new EnumTypeValidator(),
                   new PropertyValidator(),
                   new ValidationFlowFactory())
        { }

        public ClassPropertiesValidationResult Validate(Type baseType, Type toCompareType)
        {
            if (_typeValidator.IsEnum(baseType) && _typeValidator.IsEnum(toCompareType))
                return CreateResultByEnumValidation(baseType, toCompareType);

            if (!_typeValidator.ValidatePropertiesNumber(baseType, toCompareType))
                return CreateNumberOfPropertiesUnsuccessfulResult(baseType, toCompareType);

            var baseTypeProperties = baseType.GetProperties();
            var toCompareTypeProperties = toCompareType.GetProperties();

            foreach (var baseTypeProperty in baseTypeProperties)
            {
                if (!_propertyValidator.ValidateNameExistance(baseTypeProperty, toCompareTypeProperties))
                {
                    var errorMessage = CreateDistinctPropertyNamesErrorMessage(baseTypeProperty, toCompareType);
                    return CreateUnsuccessfulResult(baseType, toCompareType, errorMessage);
                }

                if (!IsValidByValidationFlow(baseTypeProperty, toCompareTypeProperties))
                    return CreateUnsuccessfulResult(baseType, toCompareType);
            }

            return CreateSuccessfulResult();
        }

        private bool IsValidByValidationFlow(PropertyInfo baseTypeProperty, IEnumerable<PropertyInfo> toCompareTypeProperties)
        {
            var toCompareTypeProperty = toCompareTypeProperties.GetByName(baseTypeProperty.Name);

            if (toCompareTypeProperty == null)
                return false;

            var validationFlow = _validationFlowFactory.Create(baseTypeProperty.PropertyType);
            return validationFlow.Validate(baseTypeProperty.PropertyType, toCompareTypeProperty.PropertyType);
        }

        private ClassPropertiesValidationResult CreateResultByEnumValidation(Type baseType, Type toCompareType)
        {
            var isValid = _enumTypeValidator.Validate(baseType, toCompareType);

            if (isValid) 
                return CreateSuccessfulResult();

            return CreateUnsuccessfulResult(baseType, toCompareType);
        }

        private ClassPropertiesValidationResult CreateSuccessfulResult()
        {
            return new ClassPropertiesValidationResult().CreateSuccessfulResult();
        }

        private ClassPropertiesValidationResult CreateNumberOfPropertiesUnsuccessfulResult(Type baseType, Type toCompareType)
        {
            const string errorMessage = "number of properties are different";
            return CreateUnsuccessfulResult(baseType, toCompareType, errorMessage);
        }

        private string CreateDistinctPropertyNamesErrorMessage(PropertyInfo baseTypeProperty, Type toCompareType)
        {
            return string.Format("property name '{0}' was not found in type '{1}'", baseTypeProperty.Name, toCompareType.Name);
        }

        private ClassPropertiesValidationResult CreateUnsuccessfulResult(Type baseType, Type toCompareType, string additionalErrorMessage = null)
        {
            var unsuccessfulResultMessage = string.Format("Failed to validate types. Type 1: '{0}', Type 2: '{1}'",
                                                          baseType.Name, toCompareType.Name);

            if (!string.IsNullOrWhiteSpace(additionalErrorMessage))
                unsuccessfulResultMessage += ". Reason: " + additionalErrorMessage;

            return new ClassPropertiesValidationResult().CreateUnsuccessfulResult(unsuccessfulResultMessage);
        }
    }
}