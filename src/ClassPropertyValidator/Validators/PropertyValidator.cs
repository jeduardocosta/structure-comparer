using System.Reflection;

namespace ClassPropertyValidator.Validators
{
    internal interface IPropertyValidator
    {
        bool ValidateName(PropertyInfo basePropertyInfo, PropertyInfo toComparePropertyInfo);
    }

    internal class PropertyValidator : IPropertyValidator
    {
        public bool ValidateName(PropertyInfo basePropertyInfo, PropertyInfo toComparePropertyInfo)
        {
            var basePropertyInfoName = basePropertyInfo.Name;
            var toComparePropertyInfoName = toComparePropertyInfo.Name;

            return basePropertyInfoName == toComparePropertyInfoName;
        }
    }
}