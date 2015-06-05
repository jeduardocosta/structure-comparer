using ClassPropertyValidator.Extensions;
using ClassPropertyValidator.Tests.Fakes.StructureB;
using FluentAssertions;
using NUnit.Framework;

namespace ClassPropertyValidator.Tests.Extensions
{
    [TestFixture]
    public class PropertyInfoExtensionsTests
    {
        [Test]
        public void GetByName_GivenAPropertyInfoCollectionAndFirstNamePropertyName_ShouldReturnPropertyInfoObject()
        {
            const string propertyName = "FirstName";

            var fakeCustomerType = typeof(FakeCustomer);
            var fakeCustomerProperties = fakeCustomerType.GetProperties();

            var propertyInfo = fakeCustomerProperties.GetByName(propertyName);

            propertyInfo.Name.Should().Be(propertyName);
        }

        [Test]
        public void GetByName_GivenAPropertyInfoCollectionAndUpperCasePropertyName_ShouldReturnNulLValue()
        {
            var propertyName = "FirstName".ToUpper();

            var fakeCustomerType = typeof(FakeCustomer);
            var fakeCustomerProperties = fakeCustomerType.GetProperties();

            var propertyInfo = fakeCustomerProperties.GetByName(propertyName);

            propertyInfo.Should().BeNull();
        }

        [Test]
        public void GetByName_GivenAPropertyInfoCollectionAndInvalidPropertyName_ShouldReturnNulLValue()
        {
            const string propertyName = "InvalidPropertyName";

            var fakeCustomerType = typeof(FakeCustomer);
            var fakeCustomerProperties = fakeCustomerType.GetProperties();

            var propertyInfo = fakeCustomerProperties.GetByName(propertyName);

            propertyInfo.Should().BeNull();
        }
    }
}