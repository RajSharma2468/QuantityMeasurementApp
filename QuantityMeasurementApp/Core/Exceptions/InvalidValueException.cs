using System;

namespace QuantityMeasurementApp.Core.Exceptions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(string message) : base(message)
        {
        }
    }
}