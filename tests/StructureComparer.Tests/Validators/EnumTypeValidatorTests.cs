using FluentAssertions;
using NUnit.Framework;
using StructureComparer.Tests.Fakes;
using StructureComparer.Validators;

namespace StructureComparer.Tests.Validators
{
    [TestFixture]
    public class EnumTypeValidatorTests
    {
        private IBaseTypeValidator _enumTypeValidator;

        [SetUp]
        public void SetUp()
        {
            _enumTypeValidator = new EnumTypeValidator();
        }

        [Test]
        public void Validate_GivenTwoIdentificalEnums_MustReturnTrueToValidationResult()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnum);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeTrue(result.DifferencesString);
        }

        [Test]
        public void Validate_GivenTwoEnumsWithDifferentNames_MustReturnFalseToValidationResult()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumDifferentNames);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeFalse(result.DifferencesString);
        }

        [Test]
        public void Validate_GivenTwoEnumsWithSameNamesButUnordered_MustReturnFalseToValidationResult()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumUnorderedNames);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeFalse(result.DifferencesString);
        }

        [Test]
        public void Validate_GivenTwoEnumsWithSameNamesButNotIdenticalValues_MustReturnFalseToValidationResult()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumWrongValues);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeFalse(result.DifferencesString);
        }

        [Test]
        public void Validate_GivenFakeEnumAndFakeEnumWrongCombination_MustReturnFalseToValidationResult()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumWrongCombination);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeFalse(result.DifferencesString);
        }

        [Test]
        public void Validate_GivenTwoEqualsNullableEnum_MustReturnTrueToValidationResult()
        {
            var baseType = typeof(FakeEnum?);
            var toCompareType = typeof(FakeEnum?);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.AreEqual.Should().BeTrue(result.DifferencesString);
        }
    }
}