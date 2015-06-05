using FluentAssertions;
using NUnit.Framework;
using System;
using ClassPropertyValidator.Models;
using ClassPropertyValidator.Tests.Fakes.StructureA;

namespace ClassPropertyValidator.Tests.Models
{
    [TestFixture]
    public class ClassPropertyValidationResultTests
    {
        private ClassPropertyValidationResult _validationResult;
        private readonly Type _fakeCustomerType = typeof(FakeCustomer);
        private readonly Type _fakeOrderType = typeof(FakeOrder);

        [SetUp]
        public void SetUp()
        {
            _validationResult = new ClassPropertyValidationResult();
        }

        [Test]
        public void AreEqual_WhenCreatingAnInstanceOfClassPropertiesValidationResultClass_MustReturnTrueToAreEqualProperty()
        {
            _validationResult.AreEqual.Should().BeTrue();
        }

        [Test]
        public void AreEqual_GivenAClassPropertyValidationResultObjectWithErrors_ShouldReturnFalseToAreaEqualProperty()
        {
            _validationResult.AddError(_fakeCustomerType, _fakeCustomerType, "error");
            
            _validationResult.AreEqual.Should().BeFalse();
        }

        [Test]
        public void GetResult_GivenAClassPropertyValidationResultObjectWithErrors_ShouldReturnFalseToAreaEqualProperty()
        {
            _validationResult.AddError(_fakeCustomerType, _fakeCustomerType, "error");

            var result = _validationResult.GetResult();

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void GetResult_GivenAClassPropertyValidationResultObjectWithoutErrors_ShouldReturnFalseToAreaEqualProperty()
        {
            var result = _validationResult.GetResult();

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void GetResult_GivenAClassPropertyValidationResultObjectWithError_ShouldReturnExpectedDifferencesString()
        {
            const string expected = "Failed to validate types. Type 1: 'FakeCustomer', Type 2: 'FakeOrder'. Reason: error";

            _validationResult.AddError(_fakeCustomerType, _fakeOrderType, "error");

            var result = _validationResult.GetResult();

            result.DifferencesString.Should().Be(expected);
        }

        [Test]
        public void GetResult_AddTwoErrorsToValidationResultObject_ShouldReturnExpectedDifferencesString()
        {
            var expected = string.Format(
                "Failed to validate types. Type 1: 'FakeCustomer', Type 2: 'FakeOrder'. Reason: error 1{0}" +
                "Failed to validate types. Type 1: 'FakeOrder', Type 2: 'FakeCustomer'. Reason: error 2", 
                Environment.NewLine);

            _validationResult.AddError(_fakeCustomerType, _fakeOrderType, "error 1");
            _validationResult.AddError(_fakeOrderType, _fakeCustomerType, "error 2");

            var result = _validationResult.GetResult();

            result.DifferencesString.Should().Be(expected);
        }
    }
}