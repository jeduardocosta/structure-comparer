namespace ClassPropertyValidator.Models
{
    public class ClassPropertiesValidationResult
    {
        public bool AreEqual { get; private set; }

        public string DifferencesString { get; private set; }

        public ClassPropertiesValidationResult()
        {
            AreEqual = true;
        }

        public ClassPropertiesValidationResult CreateSuccessfulResult()
        {
            AreEqual = true;
            return this;
        }

        public ClassPropertiesValidationResult CreateUnsuccessfulResult(string differencesString)
        {
            DifferencesString = differencesString;
            AreEqual = false;
            return this;
        }
    }
}