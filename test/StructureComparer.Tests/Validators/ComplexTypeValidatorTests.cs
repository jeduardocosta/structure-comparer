﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Tests.Fakes.StructureA;
using StructureComparer.Validators;

namespace StructureComparer.Tests.Validators
{
    [TestFixture]
    public class ComplexTypeValidatorTests
    {
        private IBaseTypeValidator _complexTypeValidator;

        [SetUp]
        public void SetUp()
        {
            _complexTypeValidator = new ComplexTypeValidator();
        }

        [TestCase(typeof(FakeCustomer), typeof(FakeCustomer))]
        [TestCase(typeof(FakeCustomerPropertyWithUpperCase), typeof(FakeCustomerPropertyWithUpperCase))]
        [TestCase(typeof(FakeOrder), typeof(FakeOrder))]
        public void Validate_GivenTwoValidComplexType_ShouldReturnTrueToValidationResult(Type baseType, Type toCompareType)
        {
            var result = _complexTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeTrue(result.DifferencesString);
        }

        [TestCase(typeof(FakeCustomer), typeof(FakeOrder))]
        public void Validate_GivenTwoDistinctComplexType_ShouldReturnFalseToValidationResult(Type baseType, Type toCompareType)
        {
            var result = _complexTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeFalse(result.DifferencesString);
        }

        [TestCase(typeof(IEnumerable<FakeCustomer>), typeof(IEnumerable<FakeCustomer>))]
        [TestCase(typeof(IEnumerable<FakeOrder>), typeof(IEnumerable<FakeOrder>))]
        [TestCase(typeof(IEnumerable<string>), typeof(IEnumerable<string>))]
        [TestCase(typeof(IEnumerable<int?>), typeof(IEnumerable<int?>))]
        public void Validate_GivenTwoValidComplexTypeCollection_ShouldReturnTrueToValidationResult(Type baseType, Type toCompareType)
        {
            var result = _complexTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeTrue(result.DifferencesString);
        }

        [TestCase(typeof(IEnumerable<FakeCustomer>), typeof(IEnumerable<FakeOrder>))]
        [TestCase(typeof(IEnumerable<int>), typeof(IEnumerable<int?>))]
        public void Validate_GivenTwoDistinctComplexTypeCollection_ShouldReturnFalseToValidationResult(Type baseType, Type toCompareType)
        {
            var result = _complexTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeFalse(result.DifferencesString);
        }
    }
}