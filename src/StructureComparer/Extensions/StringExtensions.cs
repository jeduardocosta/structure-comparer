using System;

namespace StructureComparer.Extensions
{
    public static class StringExtensions
    {
        public static string AppendPropertyName(this string value, string propertyName)
        {
            if (Contains(value, "property name"))
            {
                return string.Format("{0} from '{1}'", value, propertyName);
            }

            return string.Format("{0}. Property name: '{1}'", value, propertyName);
        }

        private static bool Contains(string value, string comparisonValue)
        {
            return value.IndexOf(comparisonValue, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}