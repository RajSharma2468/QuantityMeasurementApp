using System;

namespace QuantityMeasurementRepositoryLayer.Exceptions
{
    public class DatabaseException : Exception
    {
        public string ErrorCode { get; }
        
        public DatabaseException(string message) : base(message)
        {
            this.ErrorCode = "DATABASE_ERROR";
        }
        
        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {
            this.ErrorCode = "DATABASE_ERROR";
        }
        
        public DatabaseException(string message, string errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}