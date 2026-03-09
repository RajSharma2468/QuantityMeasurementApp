namespace QuantityMeasurementApp.Core.Exceptions;

public class InvalidUnitException : Exception
{
    public InvalidUnitException() : base("Invalid unit specified") { }
    public InvalidUnitException(string message) : base(message) { }
    public InvalidUnitException(string message, Exception innerException) : base(message, innerException) { }
}