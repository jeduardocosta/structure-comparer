using System;

namespace ClassPropertyValidator.Exception
{
    internal class ClassPropertiesValidationException : ApplicationException
    {
        public ClassPropertiesValidationException(string message)
            : base(message)
        {
            
        }
    }
}