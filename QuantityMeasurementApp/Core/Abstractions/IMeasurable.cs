namespace QuantityMeasurementApp.Core.Abstractions;

public interface IMeasurable
{
    /// <summary>
    /// Gets the conversion factor to the base unit
    /// </summary>
    double GetConversionFactor();
    
    /// <summary>
    /// Converts a value from this unit to the base unit
    /// </summary>
    double ConvertToBaseUnit(double value);
    
    /// <summary>
    /// Converts a value from the base unit to this unit
    /// </summary>
    double ConvertFromBaseUnit(double baseValue);
    
    /// <summary>
    /// Gets the display symbol of the unit
    /// </summary>
    string GetUnitSymbol();
    
    /// <summary>
    /// Gets the full name of the unit
    /// </summary>
    string GetUnitName();
}