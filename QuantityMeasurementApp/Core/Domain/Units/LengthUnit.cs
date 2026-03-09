using QuantityMeasurementApp.Core.Abstractions;

namespace QuantityMeasurementApp.Core.Domain.Units;

public enum LengthUnitType
{
    INCH,
    FEET,
    YARD,
    CENTIMETER
}

public class LengthUnit : IMeasurable
{
    private readonly LengthUnitType _unitType;
    private static readonly Dictionary<LengthUnitType, double> ConversionFactors = new()
    {
        { LengthUnitType.INCH, 1.0 / 12.0 },
        { LengthUnitType.FEET, 1.0 },
        { LengthUnitType.YARD, 3.0 },
        { LengthUnitType.CENTIMETER, 1.0 / 30.48 }
    };

    private static readonly Dictionary<LengthUnitType, string> UnitSymbols = new()
    {
        { LengthUnitType.INCH, "in" },
        { LengthUnitType.FEET, "ft" },
        { LengthUnitType.YARD, "yd" },
        { LengthUnitType.CENTIMETER, "cm" }
    };

    private static readonly Dictionary<LengthUnitType, string> UnitNames = new()
    {
        { LengthUnitType.INCH, "Inch" },
        { LengthUnitType.FEET, "Feet" },
        { LengthUnitType.YARD, "Yard" },
        { LengthUnitType.CENTIMETER, "Centimeter" }
    };

    private LengthUnit(LengthUnitType unitType)
    {
        _unitType = unitType;
    }

    public static LengthUnit Inch => new(LengthUnitType.INCH);
    public static LengthUnit Feet => new(LengthUnitType.FEET);
    public static LengthUnit Yard => new(LengthUnitType.YARD);
    public static LengthUnit Centimeter => new(LengthUnitType.CENTIMETER);

    public double GetConversionFactor() => ConversionFactors[_unitType];
    public double ConvertToBaseUnit(double value) => value * GetConversionFactor();
    public double ConvertFromBaseUnit(double baseValue) => baseValue / GetConversionFactor();
    public string GetUnitSymbol() => UnitSymbols[_unitType];
    public string GetUnitName() => UnitNames[_unitType];

    public override bool Equals(object? obj)
    {
        if (obj is not LengthUnit other) return false;
        return _unitType == other._unitType;
    }

    public override int GetHashCode() => _unitType.GetHashCode();
}