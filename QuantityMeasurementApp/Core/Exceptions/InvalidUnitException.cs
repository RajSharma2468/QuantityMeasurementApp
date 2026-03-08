using System;

namespace QuantityMeasurementApp.Core.Exceptions
{
    public class InvalidUnitException : Exception
    {
        public InvalidUnitException(string message) : base(message)
        {
        }
    }
}