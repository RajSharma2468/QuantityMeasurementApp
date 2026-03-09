using QuantityMeasurementApp.Core.Abstractions;

namespace QuantityMeasurementApp.Core.Domain.Units;

public enum WeightUnitType
{
    KILOGRAM,
    GRAM,
    POUND
}

public class WeightUnit : IMeasurable
{
    private readonly WeightUnitType _unitType;
    private static readonly Dictionary<WeightUnitType, double> ConversionFactors = new()
    {
        { WeightUnitType.KILOGRAM, 1.0 },
        { WeightUnitType.GRAM, 0.001 },
        { WeightUnitType.POUND, 0.453592 }
    };

    private static readonly Dictionary<WeightUnitType, string> UnitSymbols = new()
    {
        { WeightUnitType.KILOGRAM, "kg" },
        { WeightUnitType.GRAM, "g" },
        { WeightUnitType.POUND, "lb" }
    };

    private static readonly Dictionary<WeightUnitType, string> UnitNames = new()
    {
        { WeightUnitType.KILOGRAM, "Kilogram" },
        { WeightUnitType.GRAM, "Gram" },
        { WeightUnitType.POUND, "Pound" }
    };

    private WeightUnit(WeightUnitType unitType)
    {
        _unitType = unitType;
    }

    public static WeightUnit Kilogram => new(WeightUnitType.KILOGRAM);
    public static WeightUnit Gram => new(WeightUnitType.GRAM);
    public static WeightUnit Pound => new(WeightUnitType.POUND);

    // Weight supports arithmetic
    public bool SupportsArithmetic => true;

    public double ConvertToBaseUnit(double value) => value * ConversionFactors[_unitType];
    public double ConvertFromBaseUnit(double baseValue) => baseValue / ConversionFactors[_unitType];
    public string GetUnitSymbol() => UnitSymbols[_unitType];
    public string GetUnitName() => UnitNames[_unitType];
    public double GetConversionFactor() => ConversionFactors[_unitType];

    public override bool Equals(object? obj)
    {
        if (obj is not WeightUnit other) return false;
        return _unitType == other._unitType;
    }

    public override int GetHashCode() => _unitType.GetHashCode();
}