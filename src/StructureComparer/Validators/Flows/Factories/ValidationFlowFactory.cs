using System;
using StructureComparer.Extensions;

namespace StructureComparer.Validators.Flows.Factories
{
    internal interface IValidationFlowFactory
    {
        IValidationFlow Create(Type type);
    }

    internal class ValidationFlowFactory : IValidationFlowFactory
    {
        private readonly ITypeValidator _typeValidator;

        internal ValidationFlowFactory(ITypeValidator typeValidator)
        {
            _typeValidator = typeValidator;
        }

        public ValidationFlowFactory()
            : this(new TypeValidator())
        { }

        public IValidationFlow Create(Type type)
        {
            var typeValidator = new TypeValidator();
            var baseTypeValidator = GetBaseTypeValidator(type);

            if (_typeValidator.IsEnum(type))
                return new ValidationFlow(typeValidator, baseTypeValidator);

            if (_typeValidator.IsPrimitive(type))
                return new ValidationFlow(typeValidator, baseTypeValidator);

            return new ValidationFlow(typeValidator, baseTypeValidator);
        }

        public IBaseTypeValidator GetBaseTypeValidator(Type type)
        {
            if (_typeValidator.IsNullable(type))
                type = type.GetBaseTypeFromTypeNullable();

            if (_typeValidator.IsEnum(type))
                return new EnumTypeValidator();

            if (_typeValidator.IsPrimitive(type))
                return new PrimitiveTypeValidator();

            return new ComplexTypeValidator();
        }
    }
}