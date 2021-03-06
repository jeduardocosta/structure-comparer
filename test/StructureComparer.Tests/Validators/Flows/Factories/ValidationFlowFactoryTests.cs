﻿using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Tests.Fakes;
using StructureComparer.Tests.Fakes.StructureA;
using StructureComparer.Validators;
using StructureComparer.Validators.Flows;
using StructureComparer.Validators.Flows.Factories;

namespace StructureComparer.Tests.Validators.Flows.Factories
{
    [TestFixture]
    public class ValidationFlowFactoryTests
    {
        private ValidationFlowFactory _validationFlowFactory;

        [SetUp]
        public void SetUp()
        {
            _validationFlowFactory = new ValidationFlowFactory();
        }

        [Test]
        public void Create_GivenAnEnumType_ShouldReturnAValidationFlowInstance()
        {
            var type = typeof(FakeEnum);
            var validationFlow = _validationFlowFactory.Create(type);

            validationFlow.Should().BeOfType<ValidationFlow>();
        }

        [Test]
        public void Create_GivenAPrimitiveType_ShouldReturnAValidationFlowInstance()
        {
            var type = typeof(int);
            var validationFlow = _validationFlowFactory.Create(type);

            validationFlow.Should().BeOfType<ValidationFlow>();
        }

        [Test]
        public void GetBaseTypeValidator_GivenAComplexType_ShouldReturnComplexTypeValidatorInstance()
        {
            var type = typeof(FakeCustomer);
            var baseTypeValidator = _validationFlowFactory.GetBaseTypeValidator(type);

            baseTypeValidator.Should().BeOfType<ComplexTypeValidator>();
        }

        [Test]
        public void GetBaseTypeValidator_GivenAPrimitiveType_ShouldReturnPrimitiveTypeValidatorrInstance()
        {
            var type = typeof(int);
            var baseTypeValidator = _validationFlowFactory.GetBaseTypeValidator(type);

            baseTypeValidator.Should().BeOfType<PrimitiveTypeValidator>();
        }

        [Test]
        public void GetBaseTypeValidator_GivenANullableEnumType_ShouldReturnPrimitiveTypeValidatorInstance()
        {
            var type = typeof(int?);
            var baseTypeValidator = _validationFlowFactory.GetBaseTypeValidator(type);

            baseTypeValidator.Should().BeOfType<PrimitiveTypeValidator>();
        }

        [Test]
        public void GetBaseTypeValidator_GivenAEnumType_ShouldReturnEnumTypeValidatorInstance()
        {
            var type = typeof(FakeEnum);
            var baseTypeValidator = _validationFlowFactory.GetBaseTypeValidator(type);

            baseTypeValidator.Should().BeOfType<EnumTypeValidator>();
        }

        [Test]
        public void GetBaseTypeValidator_GivenANullableEnumType_ShouldReturnEnumTypeValidatorInstance()
        {
            var type = typeof(FakeEnum?);
            var baseTypeValidator = _validationFlowFactory.GetBaseTypeValidator(type);

            baseTypeValidator.Should().BeOfType<EnumTypeValidator>();
        }
    }
}