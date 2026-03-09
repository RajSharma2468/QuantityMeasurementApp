namespace QuantityMeasurementApp.Core.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException() : base("Invalid value specified") { }
    public InvalidValueException(string message) : base(message) { }
    public InvalidValueException(string message, Exception innerException) : base(message, innerException) { }
}