using System;
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
            var result = _comparer.Compare<FakeEnum, FakeEnumWrongValues>();

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Compare_GivenTwoTypesWithDifferentPropertyNumbers_ShouldNotValidate()
        {
            var result = _comparer.Compare<FakeOrder, FakeCustomer>();

            result.AreEqual.Should().BeFalse();
        }

        [Test]
        public void Compare_GivenTwoTypesWithDifferentPropertyNames_ShouldNotValidate()
        {
            var result = _comparer.Compare<FakeCustomer, FakeCustomerWithDifferentPropertyName>();

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

        [Test]
        public void Compare_GivenStructureAFakeClass1AndStructureBFakeClass1_ShouldReturnChainDifferences()
        {
            var expected = "Failed to validate structures. Type 1: 'Int32', Type 2: 'Int16'. Property name: 'Id'" + Environment.NewLine +
                           "Failed to validate structures. Type 1: 'FakeEnum', Type 2: 'FakeEnumDifferentNames'. Property name: 'Enum'" + Environment.NewLine +
                           "Failed to validate structures. Type 1: 'FakeEnum', Type 2: 'FakeEnumDifferentNames'. Reason: divergent enum names. Property name: 'Enum' from 'FakeClass3' from 'FakeClass2'" + Environment.NewLine +
                           "Failed to validate structures. Type 1: 'FakeClass1', Type 2: 'FakeClass1'. Reason: property name 'FullName' was not found in type 'FakeClass1'";

            var result = _comparer.Compare<StructureA.FakeClass1, StructureB.FakeClass1>();

            result.DifferencesString.Should().Be(expected);
        }
    }
}