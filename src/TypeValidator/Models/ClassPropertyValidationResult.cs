using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeValidator.Models
{
    public class ClassPropertyValidationResult
    {
        public bool AreEqual
        {
            get { return !HasErrors(); } 
        }

        public string DifferencesString { get; private set; }

        private readonly IList<string> _errors;

        public ClassPropertyValidationResult()
        {
            _errors = new List<string>();
        }

        internal ClassPropertyValidationResult GetResult()
        {
            if (HasErrors())
                DifferencesString = GenerateDifferencesString();

            return this;
        }

        internal void AddError(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        internal void AddError(Type baseType, Type toCompareType, string additionalErrorMessage = null)
        {
            var unsuccessfulResultMessage = string.Format("Failed to validate types. Type 1: '{0}', Type 2: '{1}'",
                                                          baseType.Name, toCompareType.Name);

            if (!string.IsNullOrWhiteSpace(additionalErrorMessage))
                unsuccessfulResultMessage += ". Reason: " + additionalErrorMessage;

            _errors.Add(unsuccessfulResultMessage);
        }

        internal void AddError(Type baseType, Type toCompareType, PropertyInfo property)
        {
            var additionalErrorMessage = string.Format("divergent property: {0}", property.Name);
            AddError(baseType, toCompareType, additionalErrorMessage);
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