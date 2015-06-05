using ClassPropertyValidator.Tests.Fakes;
using ClassPropertyValidator.Tests.Fakes.StructureA;
using ClassPropertyValidator.Validators.Flows;
using ClassPropertyValidator.Validators.Flows.Factories;
using NUnit.Framework;
using FluentAssertions;

namespace ClassPropertyValidator.Tests.Validators.Flows.Factories
{
    [TestFixture]
    public class ValidationFlowFactoryTests
    {
        private IValidationFlowFactory _validationFlowFactory;

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
        public void Create_GivenAComplexType_ShouldReturnAValidationFlowInstance()
        {
            var type = typeof(FakeCustomer);
            var validationFlow = _validationFlowFactory.Create(type);

            validationFlow.Should().BeOfType<ValidationFlow>();
        }
    }
}