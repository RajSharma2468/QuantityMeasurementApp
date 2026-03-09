namespace QuantityMeasurementApp.Core.Exceptions;

public class UnsupportedOperationException : Exception
{
    public UnsupportedOperationException() : base("Operation not supported for this unit type")
    {
    }

    public UnsupportedOperationException(string message) : base(message)
    {
    }

    public UnsupportedOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}