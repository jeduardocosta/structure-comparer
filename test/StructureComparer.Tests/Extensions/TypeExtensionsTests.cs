using System;
using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Extensions;
using StructureComparer.Tests.Fakes;

namespace StructureComparer.Tests.Extensions
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