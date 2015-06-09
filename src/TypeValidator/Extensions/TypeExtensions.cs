using System;
using TypeValidator.Validators;

namespace TypeValidator.Extensions
{
    public static class TypeExtensions
    {
        private static readonly ITypeValidator TypeValidator = new Validators.TypeValidator();

        public static Type GetBaseTypeFromTypeNullable(this Type type)
        {
            if (TypeValidator.IsNullable(type))
                return Nullable.GetUnderlyingType(type);

            return type;
        }
    }
}