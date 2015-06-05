using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassPropertyValidator.Models
{
    public class ClassPropertiesValidationResult
    {
        public bool AreEqual { get; private set; }
        public string DifferencesString { get; private set; }

        private readonly IList<string> _errors;

        public ClassPropertiesValidationResult()
        {
            _errors = new List<string>();
        }

        internal ClassPropertiesValidationResult GetResult()
        {
            if (HasErrors())
            {
                var differencesString = GenerateDifferencesString();
                return CreateUnsuccessfulResult(differencesString);
            }

            return CreateSuccessfulResult();
        }

        internal ClassPropertiesValidationResult CreateSuccessfulResult()
        {
            AreEqual = true;
            return this;
        }

        internal ClassPropertiesValidationResult CreateUnsuccessfulResult(string differencesString)
        {
            DifferencesString = differencesString;
            AreEqual = false;
            return this;
        }

        internal ClassPropertiesValidationResult CreateUnsuccessfulResult(Type baseType, Type toCompareType)
        {
            AddError(baseType, toCompareType);
            var differencesString = GenerateDifferencesString();
            return CreateUnsuccessfulResult(differencesString);
        }

        internal void AddError(Type baseType, Type toCompareType, string additionalErrorMessage = null)
        {
            var unsuccessfulResultMessage = string.Format("Failed to validate types. Type 1: '{0}', Type 2: '{1}'",
                                                          baseType.Name, toCompareType.Name);

            if (!string.IsNullOrWhiteSpace(additionalErrorMessage))
                unsuccessfulResultMessage += ". Reason: " + additionalErrorMessage;

            _errors.Add(unsuccessfulResultMessage);
        }

        private bool HasErrors()
        {
            return _errors.Any();
        }

        private string GenerateDifferencesString()
        {
            return string.Join(Environment.NewLine, _errors);
        }
    }
}