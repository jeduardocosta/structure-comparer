using ClassPropertyValidator.Tests.Fakes;
using ClassPropertyValidator.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace ClassPropertyValidator.Tests.Validators
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
        public void Validate_GivenTwoIdentificalEnums_MustReturnTrueToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnum);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.Should().BeTrue();
        }

        [Test]
        public void Validate_GivenTwoEnumsWithDifferentNames_MustReturnFalseToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumDifferentNames);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoEnumsWithSameNamesButUnordered_MustReturnFalseToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumUnorderedNames);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoEnumsWithSameNamesButNotIdenticalValues_MustReturnFalseToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumWrongValues);

            var result = _enumTypeValidator.Validate(baseType, toCompareType);

            result.Should().BeFalse();
        }
    }
}