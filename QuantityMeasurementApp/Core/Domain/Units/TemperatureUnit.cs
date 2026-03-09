using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Core.Domain.Units;

public enum TemperatureUnitType
{
    CELSIUS,
    FAHRENHEIT,
    KELVIN
}

public class TemperatureUnit : IMeasurable
{
    private readonly TemperatureUnitType _unitType;
    
    private static readonly Dictionary<TemperatureUnitType, string> UnitSymbols = new()
    {
        { TemperatureUnitType.CELSIUS, "°C" },
        { TemperatureUnitType.FAHRENHEIT, "°F" },
        { TemperatureUnitType.KELVIN, "K" }
    };

    private static readonly Dictionary<TemperatureUnitType, string> UnitNames = new()
    {
        { TemperatureUnitType.CELSIUS, "Celsius" },
        { TemperatureUnitType.FAHRENHEIT, "Fahrenheit" },
        { TemperatureUnitType.KELVIN, "Kelvin" }
    };

    // Temperature does not support arithmetic operations
    public bool SupportsArithmetic => false;

    private TemperatureUnit(TemperatureUnitType unitType)
    {
        _unitType = unitType;
    }

    public static TemperatureUnit Celsius => new(TemperatureUnitType.CELSIUS);
    public static TemperatureUnit Fahrenheit => new(TemperatureUnitType.FAHRENHEIT);
    public static TemperatureUnit Kelvin => new(TemperatureUnitType.KELVIN);

    // Override to validate that arithmetic operations are not supported
    public void ValidateOperationSupport(string operation)
    {
        throw new UnsupportedOperationException(
            $"Temperature does not support {operation} operations. " +
            $"Temperature values cannot be meaningfully added, subtracted, or divided."
        );
    }

    // Temperature conversions are non-linear, so we use a static helper
    public static double Convert(double value, TemperatureUnit from, TemperatureUnit to)
    {
        if (from._unitType == to._unitType)
            return value;

        // Convert to Celsius first (base unit)
        double celsius = from._unitType switch
        {
            TemperatureUnitType.CELSIUS => value,
            TemperatureUnitType.FAHRENHEIT => (value - 32) * 5.0 / 9.0,
            TemperatureUnitType.KELVIN => value - 273.15,
            _ => throw new InvalidUnitException($"Unknown temperature unit: {from._unitType}")
        };

        // Convert from Celsius to target
        return to._unitType switch
        {
            TemperatureUnitType.CELSIUS => celsius,
            TemperatureUnitType.FAHRENHEIT => celsius * 9.0 / 5.0 + 32,
            TemperatureUnitType.KELVIN => celsius + 273.15,
            _ => throw new InvalidUnitException($"Unknown temperature unit: {to._unitType}")
        };
    }

    // IMeasurable implementation
    public double ConvertToBaseUnit(double value)
    {
        // For temperature, base unit is Celsius
        return _unitType switch
        {
            TemperatureUnitType.CELSIUS => value,
            TemperatureUnitType.FAHRENHEIT => (value - 32) * 5.0 / 9.0,
            TemperatureUnitType.KELVIN => value - 273.15,
            _ => throw new InvalidUnitException($"Unknown temperature unit: {_unitType}")
        };
    }

    public double ConvertFromBaseUnit(double baseValue)
    {
        // Convert from Celsius to this unit
        return _unitType switch
        {
            TemperatureUnitType.CELSIUS => baseValue,
            TemperatureUnitType.FAHRENHEIT => baseValue * 9.0 / 5.0 + 32,
            TemperatureUnitType.KELVIN => baseValue + 273.15,
            _ => throw new InvalidUnitException($"Unknown temperature unit: {_unitType}")
        };
    }

    public string GetUnitSymbol() => UnitSymbols[_unitType];
    public string GetUnitName() => UnitNames[_unitType];
    public double GetConversionFactor() => 1.0; // Not used for temperature

    public override bool Equals(object? obj)
    {
        if (obj is not TemperatureUnit other) return false;
        return _unitType == other._unitType;
    }

    public override int GetHashCode() => _unitType.GetHashCode();
}