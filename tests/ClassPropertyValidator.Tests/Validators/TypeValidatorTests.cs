using ClassPropertyValidator.Tests.Fakes.StructureA;
using ClassPropertyValidator.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace ClassPropertyValidator.Tests.Validators
{
    [TestFixture]
    public class TypeValidatorTests
    {
        private ITypeValidator _typeValidator;

        [SetUp]
        public void SetUp()
        {
            _typeValidator = new TypeValidator();
        }

        [Test]
        public void ValidateName_GivenTwoTypesWithSameName_ShouldReturnTrueToValidationResult()
        {
            var baseType = typeof(FakeCustomer);
            var toCompareType = typeof(Fakes.StructureB.FakeCustomer);

            var result = _typeValidator.ValidateName(baseType, toCompareType);

            result.Should().BeTrue();
        }

        [Test]
        public void ValidateName_GivenTwoTypesWithDifferentNames_ShouldReturnFalseToValidationResult()
        {
            var baseType = typeof(FakeOrder);
            var toCompareType = typeof(Fakes.StructureB.FakeCustomer);

            var result = _typeValidator.ValidateName(baseType, toCompareType);

            result.Should().BeFalse();
        }

        [Test]
        public void ValidatePropertiesNumber_GivenTwoTypesWithSameNumberOfProperties_ShouldReturnTrueToValidationResult()
        {
            var baseType = typeof(FakeCustomer);
            var toCompareType = typeof(Fakes.StructureB.FakeCustomer);

            var result = _typeValidator.ValidatePropertiesNumber(baseType, toCompareType);

            result.Should().BeTrue();
        }

        [Test]
        public void ValidatePropertiesNumber_GivenTwoTypesWithDifferentNumberOfProperties_ShouldReturnTrueToValidationResult()
        {
            var baseType = typeof(FakeOrder);
            var toCompareType = typeof(Fakes.StructureB.FakeCustomer);

            var result = _typeValidator.ValidatePropertiesNumber(baseType, toCompareType);

            result.Should().BeFalse();
        }
    }
}