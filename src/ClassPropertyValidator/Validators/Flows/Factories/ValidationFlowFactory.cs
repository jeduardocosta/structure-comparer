using System;

namespace ClassPropertyValidator.Validators.Flows.Factories
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

            if (_typeValidator.IsEnum(type))
                return new ValidationFlow(typeValidator, new EnumTypeValidator());

            if (_typeValidator.IsPrimitive(type))
                return new ValidationFlow(typeValidator, new PrimitiveTypeValidator());

            return new ValidationFlow(typeValidator, new ComplexTypeValidator());
        }
    }
}