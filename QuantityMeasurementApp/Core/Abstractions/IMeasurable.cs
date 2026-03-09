using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Core.Abstractions;

// Define a delegate for checking arithmetic support
public delegate bool SupportsArithmeticDelegate();

public interface IMeasurable
{
    // Basic conversion methods (required for all units)
    double ConvertToBaseUnit(double value);
    double ConvertFromBaseUnit(double baseValue);
    string GetUnitSymbol();
    string GetUnitName();
    
    // Property to check if arithmetic is supported
    bool SupportsArithmetic { get; }
    
    // Validate if an operation is supported - default does nothing
    void ValidateOperationSupport(string operation)
    {
        // Default implementation - no validation
        // Units that don't support certain operations will override this
    }
    
    // For backward compatibility - existing units will work
    double GetConversionFactor() => 1.0;
}