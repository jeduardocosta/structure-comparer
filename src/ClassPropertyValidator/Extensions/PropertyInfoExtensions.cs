using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClassPropertyValidator.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static PropertyInfo GetByName(this IEnumerable<PropertyInfo> propertyInfoSet, string name)
        {
            return propertyInfoSet.FirstOrDefault(c => c.Name == name);
        }
    }
}