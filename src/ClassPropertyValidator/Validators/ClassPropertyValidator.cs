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
        ClassPropertyValidationResult Validate(Type baseType, Type toCompareType);
    }

    public class ClassPropertyValidator : IClassPropertyValidator
    {
        private readonly ITypeValidator _typeValidator;
        private readonly IBaseTypeValidator _enumTypeValidator;
        private readonly IPropertyValidator _propertyValidator;
        private readonly IValidationFlowFactory _validationFlowFactory;

        private readonly ClassPropertyValidationResult _validationResult;

        internal ClassPropertyValidator(ITypeValidator typeValidator, 
            IBaseTypeValidator enumTypeValidator,
            IPropertyValidator propertyValidator,
            IValidationFlowFactory validationFlowFactory)
        {
            _typeValidator = typeValidator;
            _enumTypeValidator = enumTypeValidator;
            _propertyValidator = propertyValidator;
            _validationFlowFactory = validationFlowFactory;

            _validationResult = new ClassPropertyValidationResult();
        }
        
        public ClassPropertyValidator()
            : this(new TypeValidator(), 
                   new EnumTypeValidator(),
                   new PropertyValidator(),
                   new ValidationFlowFactory())
        { }

        public ClassPropertyValidationResult Validate(Type baseType, Type toCompareType)
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
                    _validationResult.AddError(baseType, toCompareType, errorMessage);
                }
                else
                {
                    if (!IsValidByValidationFlow(baseTypeProperty, toCompareTypeProperties))
                        _validationResult.AddError(baseType, toCompareType);
                }
            }

            return _validationResult.GetResult();
        }

        private bool IsValidByValidationFlow(PropertyInfo baseTypeProperty, IEnumerable<PropertyInfo> toCompareTypeProperties)
        {
            var toCompareTypeProperty = toCompareTypeProperties.GetByName(baseTypeProperty.Name);

            if (toCompareTypeProperty == null)
                return false;

            var validationFlow = _validationFlowFactory.Create(baseTypeProperty.PropertyType);
            return validationFlow.Validate(baseTypeProperty.PropertyType, toCompareTypeProperty.PropertyType);
        }

        private ClassPropertyValidationResult CreateResultByEnumValidation(Type baseType, Type toCompareType)
        {
            var isValid = _enumTypeValidator.Validate(baseType, toCompareType);

            if (!isValid)
                _validationResult.AddError(baseType, toCompareType);

            return _validationResult.GetResult();
        }

        private ClassPropertyValidationResult CreateNumberOfPropertiesUnsuccessfulResult(Type baseType, Type toCompareType)
        {
            const string errorMessage = "number of properties are different";
            _validationResult.AddError(baseType, toCompareType, errorMessage);
            return _validationResult.GetResult();
        }

        private static string CreateDistinctPropertyNamesErrorMessage(PropertyInfo baseTypeProperty, Type toCompareType)
        {
            return string.Format("property name '{0}' was not found in type '{1}'", baseTypeProperty.Name, toCompareType.Name);
        }
    }
}