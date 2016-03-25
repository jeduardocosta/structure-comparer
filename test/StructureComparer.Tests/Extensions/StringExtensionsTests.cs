using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Extensions;

namespace StructureComparer.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void AppendPropertyName_GivenAString_ShouldReturnStringWithPropertyName()
        {
            const string propertyName = "Customer";
            const string content = "Failed to validate structures";
            const string expected = "Failed to validate structures. Property name: 'Customer'";

            var obtained = content.AppendPropertyName(propertyName);

            obtained.Should().Be(expected);
        }

        [Test]
        public void AppendPropertyName_GivenAStringWithPropertyName_ShouldAppendPropertyName()
        {
            const string propertyName = "Id";
            const string content = "Failed to validate structures. Property name: 'Customer'";
            const string expected = "Failed to validate structures. Property name: 'Customer' from 'Id'";

            var obtained = content.AppendPropertyName(propertyName);

            obtained.Should().Be(expected);
        }

        [Test]
        public void Contains_GivenValueAndComparisonValueThatExists_ShouldBeTrue()
        {
            "abc-123-def".Contains("123").Should().BeTrue();
        }

        [Test]
        public void Contains_GivenValueAndComparisonValueThatNotExists_ShouldBeFalse()
        {
            "abc-123-def".Contains("456").Should().BeFalse();
        }
    }
}