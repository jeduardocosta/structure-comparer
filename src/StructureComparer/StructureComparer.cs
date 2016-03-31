using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StructureComparer.Extensions;
using StructureComparer.Models;
using StructureComparer.Validators;
using StructureComparer.Validators.Flows.Factories;

namespace StructureComparer
{
    public static class StructureComparer
    {
        private static readonly ITypeValidator TypeValidator;
        private static readonly IBaseTypeValidator EnumTypeValidator;
        private static readonly IPropertyValidator PropertyValidator;
        private static readonly IValidationFlowFactory ValidationFlowFactory;

        static StructureComparer()
        {
            TypeValidator = new TypeValidator();
            EnumTypeValidator = new EnumTypeValidator();
            PropertyValidator = new PropertyValidator();
            ValidationFlowFactory = new ValidationFlowFactory();
        }

        public static StructureComparisonResult Compare(Type baseType, Type toCompareType)
        {
            var comparisonResult = new StructureComparisonResult();

            if (TypeValidator.IsEnum(baseType) && TypeValidator.IsEnum(toCompareType))
            {
                AppendResultByEnumValidation(comparisonResult, baseType, toCompareType);
                return comparisonResult;
            }

            if (!TypeValidator.ValidatePropertiesNumber(baseType, toCompareType))
            {
                AppendNumberOfPropertiesErrorMessage(comparisonResult, baseType, toCompareType);
            }

            var baseTypeProperties = baseType.GetProperties().ToList();
            var toCompareTypeProperties = toCompareType.GetProperties();

            baseTypeProperties.ForEach(baseTypeProperty =>
            {
                if (!PropertyValidator.ValidateNameExistance(baseTypeProperty, toCompareTypeProperties))
                {
                    var errorMessage = CreateDistinctPropertyNamesErrorMessage(baseTypeProperty, toCompareType);
                    comparisonResult.AddError(baseType, toCompareType, errorMessage);
                }
                else
                {
                    var comparisonResultFromTypeValidaton = ValidateByValidationFlow(baseTypeProperty, toCompareTypeProperties);

                    if (!comparisonResultFromTypeValidaton.AreEqual)
                    {
                        comparisonResult.AddError(comparisonResultFromTypeValidaton.DifferencesString);
                    }
                }
            });

            return comparisonResult;
        }

        public static StructureComparisonResult Compare<T1, T2>()
        {
            var type1 = typeof(T1);
            var type2 = typeof(T2);
            return Compare(type1, type2);
        }

        private static StructureComparisonResult ValidateByValidationFlow(PropertyInfo baseTypeProperty, IEnumerable<PropertyInfo> toCompareTypeProperties)
        {
            var toCompareTypeProperty = toCompareTypeProperties.GetByName(baseTypeProperty.Name);
            var validationFlow = ValidationFlowFactory.Create(baseTypeProperty.PropertyType);
            return validationFlow.Validate(baseTypeProperty.PropertyType, toCompareTypeProperty.PropertyType, baseTypeProperty.Name);
        }

        private static void AppendResultByEnumValidation(StructureComparisonResult comparisonResult, Type baseType, Type toCompareType)
        {
            var comparisonResultByEnumValidation = EnumTypeValidator.Validate(baseType, toCompareType);

            if (!comparisonResultByEnumValidation.AreEqual)
            {
                comparisonResult.AddError(comparisonResult.DifferencesString);
            }
        }

        private static void AppendNumberOfPropertiesErrorMessage(StructureComparisonResult comparisonResult, Type baseType, Type toCompareType)
        {
            const string errorMessage = "number of properties are different";
            comparisonResult.AddError(baseType, toCompareType, errorMessage);
        }

        private static string CreateDistinctPropertyNamesErrorMessage(PropertyInfo baseTypeProperty, Type toCompareType)
        {
            return $"property name '{baseTypeProperty.Name}' was not found in type '{toCompareType.Name}'";
        }
    }
}