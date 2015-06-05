using ClassPropertyValidator.Tests.Fakes;
using ClassPropertyValidator.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace ClassPropertyValidator.Tests.Validators
{
    [TestFixture]
    public class EnumTypeValidatorTests
    {
        private ITypeBaseValidator _enumTypeBaseValidator;

        [SetUp]
        public void SetUp()
        {
            _enumTypeBaseValidator = new EnumTypeBaseValidator();
        }

        [Test]
        public void Validate_GivenTwoIdentificalEnums_MustReturnTrueToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnum);

            var result = _enumTypeBaseValidator.Validate(baseType, toCompareType);

            result.Should().BeTrue();
        }

        [Test]
        public void Validate_GivenTwoEnumsWithDifferentNames_MustReturnFalseToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumDifferentNames);

            var result = _enumTypeBaseValidator.Validate(baseType, toCompareType);

            result.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoEnumsWithSameNamesButUnordered_MustReturnFalseToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumUnorderedNames);

            var result = _enumTypeBaseValidator.Validate(baseType, toCompareType);

            result.Should().BeFalse();
        }

        [Test]
        public void Validate_GivenTwoEnumsWithSameNamesButNotIdenticalValues_MustReturnFalseToValidationResultUsingEnumTypeValidator()
        {
            var baseType = typeof(FakeEnum);
            var toCompareType = typeof(FakeEnumWrongValues);

            var result = _enumTypeBaseValidator.Validate(baseType, toCompareType);

            result.Should().BeFalse();
        }
    }
}