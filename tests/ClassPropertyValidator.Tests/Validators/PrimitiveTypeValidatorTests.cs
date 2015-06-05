using System;
using FluentAssertions;
using NUnit.Framework;
using ClassPropertyValidator.Validators;

namespace ClassPropertyValidator.Tests.Validators
{
    [TestFixture]
    public class PrimitiveTypeValidatorTests
    {
        private ITypeBaseValidator _enumTypeBaseValidator;

        [SetUp]
        public void SetUp()
        {
            _enumTypeBaseValidator = new PrimitiveTypeBaseValidator();
        }

        [TestCase(typeof(int), typeof(int))]
        [TestCase(typeof(short), typeof(short))]
        [TestCase(typeof(byte), typeof(byte))]
        [TestCase(typeof(decimal), typeof(decimal))]
        [TestCase(typeof(double), typeof(double))]
        [TestCase(typeof(float), typeof(float))]
        [TestCase(typeof(bool), typeof(bool))]
        [TestCase(typeof(string), typeof(string))]
        [TestCase(typeof(char), typeof(char))]
        [TestCase(typeof(String), typeof(String))]
        [TestCase(typeof(Char), typeof(Char))]
        [TestCase(typeof(Guid), typeof(Guid))]
        [TestCase(typeof(Boolean), typeof(Boolean))]
        [TestCase(typeof(Byte), typeof(Byte))]
        [TestCase(typeof(Int16), typeof(Int16))]
        [TestCase(typeof(Int32), typeof(Int32))]
        [TestCase(typeof(Int64), typeof(Int64))]
        [TestCase(typeof(Single), typeof(Single))]
        [TestCase(typeof(Double), typeof(Double))]
        [TestCase(typeof(Decimal), typeof(Decimal))]
        [TestCase(typeof(SByte), typeof(SByte))]
        [TestCase(typeof(UInt16), typeof(UInt16))]
        [TestCase(typeof(UInt32), typeof(UInt32))]
        [TestCase(typeof(UInt64), typeof(UInt64))]
        [TestCase(typeof(DateTime), typeof(DateTime))]
        [TestCase(typeof(DateTimeOffset), typeof(DateTimeOffset))]
        [TestCase(typeof(TimeSpan), typeof(TimeSpan))]
        public void Validate_GivenTwoPrimitiveTypes_ShouldRetunTrueToValidationResult(Type baseType, Type toCompareType)
        {
            var result = _enumTypeBaseValidator.Validate(baseType, toCompareType);

            result.Should().BeTrue();
        }

        [TestCase(typeof(int?), typeof(int?))]
        [TestCase(typeof(short?), typeof(short?))]
        [TestCase(typeof(byte?), typeof(byte?))]
        [TestCase(typeof(decimal?), typeof(decimal?))]
        [TestCase(typeof(double?), typeof(double?))]
        [TestCase(typeof(float?), typeof(float?))]
        [TestCase(typeof(bool?), typeof(bool?))]
        [TestCase(typeof(char?), typeof(char?))]
        [TestCase(typeof(Char?), typeof(Char?))]
        [TestCase(typeof(Guid?), typeof(Guid?))]
        [TestCase(typeof(Boolean?), typeof(Boolean?))]
        [TestCase(typeof(Byte?), typeof(Byte?))]
        [TestCase(typeof(Int16?), typeof(Int16?))]
        [TestCase(typeof(Int32?), typeof(Int32?))]
        [TestCase(typeof(Int64?), typeof(Int64?))]
        [TestCase(typeof(Single?), typeof(Single?))]
        [TestCase(typeof(Double?), typeof(Double?))]
        [TestCase(typeof(Decimal?), typeof(Decimal?))]
        [TestCase(typeof(SByte?), typeof(SByte?))]
        [TestCase(typeof(UInt16?), typeof(UInt16?))]
        [TestCase(typeof(UInt32?), typeof(UInt32?))]
        [TestCase(typeof(UInt64?), typeof(UInt64?))]
        [TestCase(typeof(DateTime?), typeof(DateTime?))]
        [TestCase(typeof(DateTimeOffset?), typeof(DateTimeOffset?))]
        [TestCase(typeof(TimeSpan?), typeof(TimeSpan?))]
        public void Validate_GivenTwoPrimitiveNullableTypes_ShouldRetunTrueToValidationResult(Type baseType, Type toCompareType)
        {
            var result = _enumTypeBaseValidator.Validate(baseType, toCompareType);

            result.Should().BeTrue();
        }
    }
}