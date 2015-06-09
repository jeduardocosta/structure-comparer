using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Tests.Fakes.StructureA;
using StructureComparer.Validators;

namespace StructureComparer.Tests.Validators
{
    [TestFixture]
    public class PropertyValidatorTests
    {
        private IPropertyValidator _propertyValidator;

        private FakeCustomer _fakeCustomer;
        private FakeCustomerPropertyWithUpperCase _fakeCustomerPropertyWithUpperCase;

        private Type _fakeCustomerType;
        private Type _fakeOrderType;

        [SetUp]
        public void SetUp()
        {
            _propertyValidator = new PropertyValidator();

            _fakeCustomer = new FakeCustomer();
            _fakeCustomerPropertyWithUpperCase = new FakeCustomerPropertyWithUpperCase();

            _fakeCustomerType = typeof(FakeCustomer);
            _fakeOrderType = typeof(FakeOrder);
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

        [Test]
        public void ValidateNameExistance_GivenFirstNamePropertyAndValidPropertiesCollections_ShouldReturnTrueToValidationResult()
        {
            var property = _fakeCustomerType.GetProperty("FirstName");
            var properties = new List<PropertyInfo>
            {
                _fakeCustomerType.GetProperty("FirstName"),
                _fakeCustomerType.GetProperty("LastName"),
            };

            var result = _propertyValidator.ValidateNameExistance(property, properties);

            result.Should().BeTrue();
        }

        [Test]
        public void ValidateNameExistance_GivenIdPropertyAndInvalidPropertiesCollections_ShouldReturnFalseToValidationResult()
        {
            var property = _fakeOrderType.GetProperty("Id");
            var properties = new List<PropertyInfo>
            {
                _fakeCustomerType.GetProperty("FirstName"),
                _fakeCustomerType.GetProperty("LastName"),
            };

            var result = _propertyValidator.ValidateNameExistance(property, properties);

            result.Should().BeFalse();
        }
    }
}