using System;

namespace ModelLayer.Exceptions
{
    public class QuantityMeasurementException : Exception
    {
        public string ErrorCode { get; }

        public QuantityMeasurementException(string message) : base(message)
        {
        }

        public QuantityMeasurementException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        public QuantityMeasurementException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}