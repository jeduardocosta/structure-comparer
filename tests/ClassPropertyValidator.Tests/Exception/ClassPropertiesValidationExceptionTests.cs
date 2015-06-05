using ClassPropertyValidator.Exception;
using NUnit.Framework;

namespace ClassPropertyValidator.Tests.Exception
{
    [TestFixture]
    public class ClassPropertiesValidationExceptionTests
    {
        private const string ErrorMessage = "error-message-123";

        [Test]
        [ExpectedException(typeof(ClassPropertiesValidationException), ExpectedMessage = ErrorMessage)]
        public void ClassPropertiesValidationException_WhenThrowAnException_MustReturnExpectedErrorMessage()
        {
            throw new ClassPropertiesValidationException(ErrorMessage);
        }
    }
}