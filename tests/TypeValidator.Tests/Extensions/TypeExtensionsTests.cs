using NUnit.Framework;
using System;
using FluentAssertions;
using TypeValidator.Extensions;
using TypeValidator.Tests.Fakes;

namespace TypeValidator.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [TestCase(typeof(int?), typeof(int))]
        [TestCase(typeof(DateTime?), typeof(DateTime))]
        [TestCase(typeof(string), typeof(string))]
        [TestCase(typeof(FakeEnum?), typeof(FakeEnum))]
        public void GetBaseTypeFromNullable_GivenANullableType_MustReturnExpectedBaseType(Type baseType, Type expectedType)
        {
            var obtained = baseType.GetBaseTypeFromTypeNullable();

            obtained.Should().Be(expectedType);
        }
    }
}