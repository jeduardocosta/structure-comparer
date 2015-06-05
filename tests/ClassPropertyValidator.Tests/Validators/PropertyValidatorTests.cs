using System.Linq;
using ClassPropertyValidator.Tests.Fakes.StructureA;
using ClassPropertyValidator.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace ClassPropertyValidator.Tests.Validators
{
    [TestFixture]
    public class PropertyValidatorTests
    {
        private IPropertyValidator _propertyValidator;

        private FakeCustomer _fakeCustomer;
        private FakeCustomerPropertyWithUpperCase _fakeCustomerPropertyWithUpperCase;

        [SetUp]
        public void SetUp()
        {
            _propertyValidator = new PropertyValidator();

            _fakeCustomer = new FakeCustomer();
            _fakeCustomerPropertyWithUpperCase = new FakeCustomerPropertyWithUpperCase();
        }

        [Test]
        public void ValidateName_GivenTwoPropertyInfoWithSameName_ShouldReturnTrueToValidationResult()
        {
            var propertyInfo = _fakeCustomer.GetType().GetProperties().First();

            var basePropertyInfo = propertyInfo;
            var toComparePropertyInfo = propertyInfo;

            var result = _propertyValidator.ValidateName(basePropertyInfo, toComparePropertyInfo);

            result.Should().BeTrue();
        }

        [Test]
        public void ValidateName_GivenTwoPropertyInfoWithSameNameButDifferentCase_ShouldReturnFalseToValidationResult()
        {
            var firstFakeCustomerPropertyInfo = _fakeCustomer.GetType().GetProperties().First();
            var firstFakeCustomerPropertyWithUpperCasePropertyInfo = _fakeCustomerPropertyWithUpperCase.GetType().GetProperties().First();

            var basePropertyInfo = firstFakeCustomerPropertyInfo;
            var toComparePropertyInfo = firstFakeCustomerPropertyWithUpperCasePropertyInfo;

            var result = _propertyValidator.ValidateName(basePropertyInfo, toComparePropertyInfo);

            result.Should().BeFalse();
        }

        [Test]
        public void ValidateName_GivenTwoPropertyInfoWithDifferentName_ShouldReturnFalseToValidationResult()
        {
            var firstPropertyInfo = _fakeCustomer.GetType().GetProperties().First();
            var lastPropertyInfo = _fakeCustomer.GetType().GetProperties().Last();

            var basePropertyInfo = firstPropertyInfo;
            var toComparePropertyInfo = lastPropertyInfo;

            var result = _propertyValidator.ValidateName(basePropertyInfo, toComparePropertyInfo);

            result.Should().BeFalse();
        }
    }
}