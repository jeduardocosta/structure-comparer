using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Tests.Fakes;
using StructureComparer.Tests.Fakes.StructureA;

namespace StructureComparer.Tests
{
    [TestFixture]
    public class StructureComparerTests
    {
        private IStructureComparer _comparer;

        [SetUp]
        public void SetUp()
        {
            _comparer = new StructureComparer();
        }

        [Test]
        public void Validate_GivenTwoIdenticalEnums_ShouldValidate()
        {
            var fakeEnumA = typeof(FakeEnum);
            var fakeEnumB = typeof(FakeEnum);

            var result = _comparer.Compare(fakeEnumA, fakeEnumB);

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void Validate_GivenTwoDifferentEnums_ShouldNotValidate()
        {
            var fakeEnumA = typeof(FakeEnum);
            var fakeEnumB = typeof(FakeEnumWrongValues);

            var result = _comparer.Compare(fakeEnumA, fakeEnumB);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoTypesWithDifferentPropertyNumbers_ShouldNotValidate()
        {
            var fakeOrder = typeof(FakeOrder);
            var fakeCustomer = typeof(FakeCustomer);

            var result = _comparer.Compare(fakeOrder, fakeCustomer);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoTypesWithDifferentPropertyNames_ShouldNotValidate()
        {
            var fakeCustomer = typeof(FakeCustomer);
            var fakeCustomerWithDifferentPropertyName = typeof(FakeCustomerWithDifferentPropertyName);

            var result = _comparer.Compare(fakeCustomer, fakeCustomerWithDifferentPropertyName);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoFakeCustomerObjectTypes_ShouldValidate()
        {
            var fakeCustomerA = typeof(FakeCustomer);
            var fakeCustomerB = typeof(FakeCustomer);

            var result = _comparer.Compare(fakeCustomerA, fakeCustomerB);

            result.AreEqual.Should().BeTrue();
        }
    }
}