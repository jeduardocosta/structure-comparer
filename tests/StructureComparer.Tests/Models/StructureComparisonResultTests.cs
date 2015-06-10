using System;
using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Models;
using StructureComparer.Tests.Fakes.StructureA;

namespace StructureComparer.Tests.Models
{
    [TestFixture]
    public class StructureComparisonResultTests
    {
        private StructureComparisonResult _comparisonResult;

        private readonly Type _fakeCustomerType = typeof(FakeCustomer);
        private readonly Type _fakeOrderType = typeof(FakeOrder);

        [SetUp]
        public void SetUp()
        {
            _comparisonResult = new StructureComparisonResult();
        }

        [Test]
        public void AreEqual_WhenCreatingAnInstanceOfClassPropertiesValidationResultClass_MustReturnTrueToAreEqualProperty()
        {
            _comparisonResult.AreEqual.Should().BeTrue();
        }

        [Test]
        public void AreEqual_GivenAClassPropertyValidationResultObjectWithErrors_ShouldReturnFalseToAreaEqualProperty()
        {
            _comparisonResult.AddError(_fakeCustomerType, _fakeCustomerType, "error");
            
            _comparisonResult.AreEqual.Should().BeFalse();
        }

        [Test]
        public void GetResult_GivenAClassPropertyValidationResultObjectWithErrors_ShouldReturnFalseToAreaEqualProperty()
        {
            _comparisonResult.AddError(_fakeCustomerType, _fakeCustomerType, "error");

            var result = _comparisonResult.GetResult();

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void GetResult_GivenAClassPropertyValidationResultObjectWithoutErrors_ShouldReturnFalseToAreaEqualProperty()
        {
            var result = _comparisonResult.GetResult();

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void GetResult_GivenAClassPropertyValidationResultObjectWithError_ShouldReturnExpectedDifferencesString()
        {
            const string expected = "Failed to validate structures. Type 1: 'FakeCustomer', Type 2: 'FakeOrder'. Reason: error";

            _comparisonResult.AddError(_fakeCustomerType, _fakeOrderType, "error");

            var result = _comparisonResult.GetResult();

            result.DifferencesString.Should().Be(expected);
        }

        [Test]
        public void GetResult_AddTwoErrorsToValidationResultObject_ShouldReturnExpectedDifferencesString()
        {
            var expected = string.Format(
                "Failed to validate structures. Type 1: 'FakeCustomer', Type 2: 'FakeOrder'. Reason: error 1{0}" +
                "Failed to validate structures. Type 1: 'FakeOrder', Type 2: 'FakeCustomer'. Reason: error 2", 
                Environment.NewLine);

            _comparisonResult.AddError(_fakeCustomerType, _fakeOrderType, "error 1");
            _comparisonResult.AddError(_fakeOrderType, _fakeCustomerType, "error 2");

            var result = _comparisonResult.GetResult();

            result.DifferencesString.Should().Be(expected);
        }
    }
}