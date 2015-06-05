using System;
using System.Collections.Generic;
using ClassPropertyValidator.Tests.Fakes;
using ClassPropertyValidator.Tests.Fakes.StructureA;
using NUnit.Framework;
using ClassPropertyValidator.Validators;
using FluentAssertions;

namespace ClassPropertyValidator.Tests.Validators
{
    [TestFixture]
    public class TypeTypeValidatorTests
    {
        private ITypeTypeValidator _typeTypeValidator;

        [SetUp]
        public void SetUp()
        {
            _typeTypeValidator = new TypeTypeValidator();
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(short))]
        [TestCase(typeof(byte))]
        [TestCase(typeof(decimal))]
        [TestCase(typeof(double))]
        [TestCase(typeof(float))]
        [TestCase(typeof(bool))]
        [TestCase(typeof(string))]
        [TestCase(typeof(char))]
        [TestCase(typeof(String))]
        [TestCase(typeof(Char))]
        [TestCase(typeof(Guid))]
        [TestCase(typeof(Boolean))]
        [TestCase(typeof(Byte))]
        [TestCase(typeof(Int16))]
        [TestCase(typeof(Int32))]
        [TestCase(typeof(Int64))]
        [TestCase(typeof(Single))]
        [TestCase(typeof(Double))]
        [TestCase(typeof(Decimal))]
        [TestCase(typeof(SByte))]
        [TestCase(typeof(UInt16))]
        [TestCase(typeof(UInt32))]
        [TestCase(typeof(UInt64))]
        [TestCase(typeof(DateTime))]
        [TestCase(typeof(DateTimeOffset))]
        [TestCase(typeof(TimeSpan))]
        public void IsPrimitive_GivenAValidPrimitiveType_MustReturnTrueToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsPrimitive(type);

            result.Should().BeTrue();
        }

        [TestCase(typeof(int?))]
        [TestCase(typeof(short?))]
        [TestCase(typeof(byte?))]
        [TestCase(typeof(decimal?))]
        [TestCase(typeof(double?))]
        [TestCase(typeof(float?))]
        [TestCase(typeof(bool?))]
        [TestCase(typeof(char?))]
        [TestCase(typeof(Char?))]
        [TestCase(typeof(Guid?))]
        [TestCase(typeof(Boolean?))]
        [TestCase(typeof(Byte?))]
        [TestCase(typeof(Int16?))]
        [TestCase(typeof(Int32?))]
        [TestCase(typeof(Int64?))]
        [TestCase(typeof(Single?))]
        [TestCase(typeof(Double?))]
        [TestCase(typeof(Decimal?))]
        [TestCase(typeof(SByte?))]
        [TestCase(typeof(UInt16?))]
        [TestCase(typeof(UInt32?))]
        [TestCase(typeof(UInt64?))]
        [TestCase(typeof(DateTime?))]
        [TestCase(typeof(DateTimeOffset?))]
        [TestCase(typeof(TimeSpan?))]
        public void IsPrimitive_GivenANullablePrimitiveType_MustReturnTrueToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsPrimitive(type);

            result.Should().BeTrue();
        }

        [TestCase(typeof(FakeEnum))]
        [TestCase(typeof(FakeEnumDifferentNames))]
        [TestCase(typeof(FakeCustomer))]
        public void IsPrimitive_GivenAnInvalidPrimitiveType_MustReturnFalseToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsPrimitive(type);

            result.Should().BeFalse();
        }

        [TestCase(typeof(FakeEnum))]
        [TestCase(typeof(FakeEnumDifferentNames))]
        [TestCase(typeof(FakeEnumUnorderedNames))]
        [TestCase(typeof(FakeEnumWrongValues))]
        public void IsEnum_GivenAValidEnum_MustReturnTrueToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsEnum(type);

            result.Should().BeTrue();
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(string))]
        [TestCase(typeof(FakeCustomer))]
        public void IsEnum_GivenAnInvalidEnum_MustReturnFalseToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsEnum(type);

            result.Should().BeFalse();
        }

        [TestCase(typeof(FakeCustomer))]
        [TestCase(typeof(FakeCustomerPropertyWithUpperCase))]
        [TestCase(typeof(FakeOrder))]
        public void IsComplexType_GivenAValidComplexType_MustReturnFalseToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsComplexType(type);

            result.Should().BeTrue();
        }

        [TestCase(typeof(string))]
        [TestCase(typeof(Double))]
        [TestCase(typeof(FakeEnum))]
        public void IsComplexType_GivenAnInvalidComplexType_MustReturnFalseToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsComplexType(type);

            result.Should().BeFalse();
        }

        [TestCase(typeof(IEnumerable<FakeCustomer>))]
        [TestCase(typeof(ICollection<FakeCustomer>))]
        [TestCase(typeof(IList<FakeCustomer>))]
        [TestCase(typeof(List<FakeCustomer>))]
        [TestCase(typeof(Array))]
        public void IsEnumerableType_GivenATypeInheritedFromIEnumerableType_MustReturnTrueToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsEnumerableType(type);

            result.Should().BeTrue();
        }

        [TestCase(typeof(string))]
        [TestCase(typeof(DateTime?))]
        [TestCase(typeof(double))]
        [TestCase(typeof(FakeEnum))]
        [TestCase(typeof(FakeCustomer))]
        public void IsEnumerableType_GivenAInvalid_MustReturnFalseToValidationResult(Type type)
        {
            var result = _typeTypeValidator.IsEnumerableType(type);

            result.Should().BeFalse();
        }
    }
}