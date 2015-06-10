using System;
using System.Collections.Generic;
using System.Reflection;
using StructureComparer.Extensions;
using StructureComparer.Models;
using StructureComparer.Validators;
using StructureComparer.Validators.Flows.Factories;

namespace StructureComparer
{
    public interface IStructureComparer
    {
        StructureComparisonResult Compare(Type baseType, Type toCompareType);
        StructureComparisonResult Compare<T1, T2>();
    }

    public class StructureComparer : IStructureComparer
    {
        private readonly ITypeValidator _typeValidator;
        private readonly IBaseTypeValidator _enumTypeValidator;
        private readonly IPropertyValidator _propertyValidator;
        private readonly IValidationFlowFactory _validationFlowFactory;

        private readonly StructureComparisonResult _comparisonResult;

        internal StructureComparer(ITypeValidator typeValidator, 
            IBaseTypeValidator enumTypeValidator,
            IPropertyValidator propertyValidator,
            IValidationFlowFactory validationFlowFactory)
        {
            _typeValidator = typeValidator;
            _enumTypeValidator = enumTypeValidator;
            _propertyValidator = propertyValidator;
            _validationFlowFactory = validationFlowFactory;

            _comparisonResult = new StructureComparisonResult();
        }
        
        public StructureComparer()
            : this(new TypeValidator(), 
                   new EnumTypeValidator(),
                   new PropertyValidator(),
                   new ValidationFlowFactory())
        { }

        public StructureComparisonResult Compare(Type baseType, Type toCompareType)
        {
            if (_typeValidator.IsEnum(baseType) && _typeValidator.IsEnum(toCompareType))
                return CreateResultByEnumValidation(baseType, toCompareType);

            if (!_typeValidator.ValidatePropertiesNumber(baseType, toCompareType))
                _comparisonResult.AddError(CreateNumberOfPropertiesUnsuccessfulResult(baseType, toCompareType).DifferencesString);

            var baseTypeProperties = baseType.GetProperties();
            var toCompareTypeProperties = toCompareType.GetProperties();

            foreach (var baseTypeProperty in baseTypeProperties)
            {
                if (!_propertyValidator.ValidateNameExistance(baseTypeProperty, toCompareTypeProperties))
                {
                    var errorMessage = CreateDistinctPropertyNamesErrorMessage(baseTypeProperty, toCompareType);
                    _comparisonResult.AddError(baseType, toCompareType, errorMessage);
                }
                else
                {
                    var comparisonResultFromTypeValidaton = ValidateByValidationFlow(baseTypeProperty, toCompareTypeProperties);
                    if (!comparisonResultFromTypeValidaton.AreEqual)
                        _comparisonResult.AddError(comparisonResultFromTypeValidaton.DifferencesString);
                }
            }

            return _comparisonResult;
        }

        public StructureComparisonResult Compare<T1, T2>()
        {
            var type1 = typeof(T1);
            var type2 = typeof(T2);
            return Compare(type1, type2);
        }

        private StructureComparisonResult ValidateByValidationFlow(PropertyInfo baseTypeProperty, IEnumerable<PropertyInfo> toCompareTypeProperties)
        {
            var toCompareTypeProperty = toCompareTypeProperties.GetByName(baseTypeProperty.Name);
            var validationFlow = _validationFlowFactory.Create(baseTypeProperty.PropertyType);
            return validationFlow.Validate(baseTypeProperty.PropertyType, toCompareTypeProperty.PropertyType, baseTypeProperty.Name);
        }

        private StructureComparisonResult CreateResultByEnumValidation(Type baseType, Type toCompareType)
        {
            var comparisonResult = _enumTypeValidator.Validate(baseType, toCompareType);

            if (!comparisonResult.AreEqual)
                _comparisonResult.AddError(comparisonResult.DifferencesString);

            return _comparisonResult;
        }

        private StructureComparisonResult CreateNumberOfPropertiesUnsuccessfulResult(Type baseType, Type toCompareType)
        {
            const string errorMessage = "number of properties are different";
            _comparisonResult.AddError(baseType, toCompareType, errorMessage);
            return _comparisonResult;
        }

        private static string CreateDistinctPropertyNamesErrorMessage(PropertyInfo baseTypeProperty, Type toCompareType)
        {
            return string.Format("property name '{0}' was not found in type '{1}'", baseTypeProperty.Name, toCompareType.Name);
        }
    }
}