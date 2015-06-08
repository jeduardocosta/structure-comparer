using FluentAssertions;
using NUnit.Framework;
using TypeValidator.Tests.Fakes;
using TypeValidator.Tests.Fakes.StructureA;
using TypeValidator.Validators;

namespace TypeValidator.Tests.Validators
{
    [TestFixture]
    public class ClassPropertyValidatorTests
    {
        private IClassPropertyValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new ClassPropertyValidator();
        }

        [Test]
        public void Validate_GivenTwoIdenticalEnums_ShouldValidate()
        {
            var fakeEnumA = typeof(FakeEnum);
            var fakeEnumB = typeof(FakeEnum);

            var result = _validator.Validate(fakeEnumA, fakeEnumB);

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void Validate_GivenTwoDifferentEnums_ShouldNotValidate()
        {
            var fakeEnumA = typeof(FakeEnum);
            var fakeEnumB = typeof(FakeEnumWrongValues);

            var result = _validator.Validate(fakeEnumA, fakeEnumB);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoTypesWithDifferentPropertyNumbers_ShouldNotValidate()
        {
            var fakeOrder = typeof(FakeOrder);
            var fakeCustomer = typeof(FakeCustomer);

            var result = _validator.Validate(fakeOrder, fakeCustomer);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoTypesWithDifferentPropertyNames_ShouldNotValidate()
        {
            var fakeCustomer = typeof(FakeCustomer);
            var fakeCustomerWithDifferentPropertyName = typeof(FakeCustomerWithDifferentPropertyName);

            var result = _validator.Validate(fakeCustomer, fakeCustomerWithDifferentPropertyName);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoFakeCustomerObjectTypes_ShouldValidate()
        {
            var fakeCustomerA = typeof(FakeCustomer);
            var fakeCustomerB = typeof(FakeCustomer);

            var result = _validator.Validate(fakeCustomerA, fakeCustomerB);

            result.AreEqual.Should().BeTrue();
        }
    }
}