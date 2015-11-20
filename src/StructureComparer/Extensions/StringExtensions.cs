using System;

namespace StructureComparer.Extensions
{
    public static class StringExtensions
    {
        public static string AppendPropertyName(this string value, string propertyName)
        {
            if (Contains(value, "property name"))
            {
                return $"{value} from '{propertyName}'";
            }

            return $"{value}. Property name: '{propertyName}'";
        }

        private static bool Contains(string value, string comparisonValue)
        {
            return value.IndexOf(comparisonValue, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}