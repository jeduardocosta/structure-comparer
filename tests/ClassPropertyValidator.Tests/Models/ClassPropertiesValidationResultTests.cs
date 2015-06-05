using ClassPropertyValidator.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ClassPropertyValidator.Tests.Models
{
    [TestFixture]
    public class ClassPropertiesValidationResultTests
    {
        [Test]
        public void Constructor_WhenCreatingAnInstanceOfClassPropertiesValidationResultClass_MustReturnTrueToAreEqualProperty()
        {
            var classPropertiesValidationResult = new ClassPropertiesValidationResult();

            classPropertiesValidationResult.AreEqual.Should().BeTrue();
        }

        [Test]
        public void CreateSuccessfulResult_GivenAClassPropertiesValidationResultObject_MustReturnTrueToAreEqualProperty()
        {
            var classPropertiesValidationResult = new ClassPropertiesValidationResult();
            classPropertiesValidationResult.CreateSuccessfulResult();

            classPropertiesValidationResult.AreEqual.Should().BeTrue();
        }

        [Test]
        public void CreateUnsuccessfulResult_GivenAClassPropertiesValidationResultObject_MustReturnFalseToAreEqualProperty()
        {
            var classPropertiesValidationResult = new ClassPropertiesValidationResult();
            classPropertiesValidationResult.CreateUnsuccessfulResult(It.IsAny<string>());

            classPropertiesValidationResult.AreEqual.Should().BeFalse();
        }

        [Test]
        public void CreateUnsuccessfulResult_GivenDifferencesStringContent_MustReturnDifferencesStringEntryValue()
        {
            const string differencesString = "differencesString-123";

            var classPropertiesValidationResult = new ClassPropertiesValidationResult();
            classPropertiesValidationResult.CreateUnsuccessfulResult(differencesString);

            classPropertiesValidationResult.DifferencesString.Should().Be(differencesString);
        }
    }
}
