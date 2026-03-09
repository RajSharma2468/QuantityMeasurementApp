namespace QuantityMeasurementApp.Core.Exceptions;

public class DivisionByZeroException : Exception
{
    public DivisionByZeroException() : base("Cannot divide by zero") { }
    public DivisionByZeroException(string message) : base(message) { }
    public DivisionByZeroException(string message, Exception innerException) : base(message, innerException) { }
}