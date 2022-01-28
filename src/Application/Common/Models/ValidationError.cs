namespace WhatBug.Application.Common.Models
{
    public class ValidationError
    {
        public string Message { get; }
        public string PropertyName { get; }
        public object AttemptedValue { get; }

        public ValidationError(string message, string propertyName, object attemptedValue)
        {
            Message = message;
            PropertyName = propertyName;
            AttemptedValue = attemptedValue;
        }
    }
}
