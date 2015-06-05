using System;

namespace ClassPropertyValidator.Validators.Flows.Factories
{
    internal interface IValidationFlowFactory
    {
        IValidationFlow Create(Type type);
    }

    internal class ValidationFlowFactory : IValidationFlowFactory
    {
        private readonly ITypeTypeValidator _typeValidator;

        public ValidationFlowFactory(ITypeTypeValidator typeValidator)
        {
            _typeValidator = typeValidator;
        }

        public ValidationFlowFactory()
            : this(new TypeTypeValidator())
        {
            
        }

        public IValidationFlow Create(Type type)
        {
            var typeValidator = new TypeValidator();

            if (_typeValidator.IsEnum(type))
                return new ValidationFlow(typeValidator, new EnumTypeBaseValidator());

            if (_typeValidator.IsPrimitive(type))
                return new ValidationFlow(typeValidator, new PrimitiveTypeBaseValidator());

            return new ValidationFlow(typeValidator, new ComplexTypeBaseValidator());
        }
    }
}