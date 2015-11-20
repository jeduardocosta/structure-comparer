using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StructureComparer.Models
{
    public class StructureComparisonResult
    {
        public bool AreEqual => !HasErrors();

        public string DifferencesString => GenerateDifferencesString();

        private readonly IList<string> _errors;

        public StructureComparisonResult()
        {
            _errors = new List<string>();
        }

        internal void AddError(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        internal void AddError(Type baseType, Type toCompareType, string additionalErrorMessage = null)
        {
            var unsuccessfulResultMessage = $"Failed to validate structures. Type 1: '{baseType.Name}', Type 2: '{toCompareType.Name}'";

            if (!string.IsNullOrWhiteSpace(additionalErrorMessage))
            {
                unsuccessfulResultMessage += ". Reason: " + additionalErrorMessage;
            }

            _errors.Add(unsuccessfulResultMessage);
        }

        internal void AddError(Type baseType, Type toCompareType, PropertyInfo property)
        {
            var additionalErrorMessage = $"divergent property: {property.Name}";
            AddError(baseType, toCompareType, additionalErrorMessage);
        }

        private bool HasErrors()
        {
            return _errors.Any();
        }

        private string GenerateDifferencesString()
        {
            if (_errors.Any())
            { 
                return string.Join(Environment.NewLine, _errors);
            }

            return string.Empty;
        }
    }
}