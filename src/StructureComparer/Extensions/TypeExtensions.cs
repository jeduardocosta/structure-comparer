using System;
using StructureComparer.Validators;

namespace StructureComparer.Extensions
{
    public static class TypeExtensions
    {
        private static readonly ITypeValidator TypeValidator = new TypeValidator();

        public static Type GetBaseTypeFromTypeNullable(this Type type)
        {
            if (TypeValidator.IsNullable(type))
            { 
                return Nullable.GetUnderlyingType(type);
            }

            return type;
        }
    }
}