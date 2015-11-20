using System.Collections.Generic;
using System.Reflection;

namespace StructureComparer.Validators
{
    internal interface IPropertyValidator
    {
        bool ValidateName(PropertyInfo baseProperty, PropertyInfo toCompareProperty);
        bool ValidateNameExistance(PropertyInfo baseProperty, IEnumerable<PropertyInfo> toCompareTypeProperties);
    }
}