using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Tests.Fakes;
using StructureComparer.Tests.Fakes.StructureA;
using StructureA = StructureComparer.Tests.Fakes.StructureA;
using StructureB = StructureComparer.Tests.Fakes.StructureB;

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
        public void Compare_GivenTwoIdenticalEnums_ShouldValidate()
        {
            var fakeEnumA = typeof(FakeEnum);
            var fakeEnumB = typeof(FakeEnum);

            var result = _comparer.Compare(fakeEnumA, fakeEnumB);

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void Compare_GivenTwoDifferentEnums_ShouldNotValidate()
        {
            var fakeEnumA = typeof(FakeEnum);
            var fakeEnumB = typeof(FakeEnumWrongValues);

            var result = _comparer.Compare(fakeEnumA, fakeEnumB);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Compare_GivenTwoTypesWithDifferentPropertyNumbers_ShouldNotValidate()
        {
            var fakeOrder = typeof(FakeOrder);
            var fakeCustomer = typeof(FakeCustomer);

            var result = _comparer.Compare(fakeOrder, fakeCustomer);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Compare_GivenTwoTypesWithDifferentPropertyNames_ShouldNotValidate()
        {
            var fakeCustomer = typeof(FakeCustomer);
            var fakeCustomerWithDifferentPropertyName = typeof(FakeCustomerWithDifferentPropertyName);

            var result = _comparer.Compare(fakeCustomer, fakeCustomerWithDifferentPropertyName);

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Compare_GivenTwoFakeCustomerObjectTypes_ShouldValidate()
        {
            var fakeCustomerA = typeof(FakeCustomer);
            var fakeCustomerB = typeof(FakeCustomer);

            var result = _comparer.Compare(fakeCustomerA, fakeCustomerB);

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void Compare_GivenTwoIntegerObjects_ShouldValidate()
        {
            var result = _comparer.Compare<int, int>();

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void Compare_GivenTwoFakeEnumObjects_ShouldValidate()
        {
            var result = _comparer.Compare<FakeEnum, FakeEnum>();

            result.AreEqual.Should().BeTrue();
        }

        [Test]
        public void Compare_GivenStructureAFakeCustomerAndStructureBFakeCustomer_ShouldValidate()
        {
            var result = _comparer.Compare<StructureA.FakeCustomer, StructureB.FakeCustomer>();

            result.AreEqual.Should().BeTrue();
        }
    }
}