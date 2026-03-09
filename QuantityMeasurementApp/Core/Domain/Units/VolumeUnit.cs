using QuantityMeasurementApp.Core.Abstractions;

namespace QuantityMeasurementApp.Core.Domain.Units;

public enum VolumeUnitType
{
    LITRE,
    MILLILITRE,
    GALLON
}

public class VolumeUnit : IMeasurable
{
    private readonly VolumeUnitType _unitType;
    private static readonly Dictionary<VolumeUnitType, double> ConversionFactors = new()
    {
        { VolumeUnitType.LITRE, 1.0 },
        { VolumeUnitType.MILLILITRE, 0.001 },
        { VolumeUnitType.GALLON, 3.78541 }
    };

    private static readonly Dictionary<VolumeUnitType, string> UnitSymbols = new()
    {
        { VolumeUnitType.LITRE, "L" },
        { VolumeUnitType.MILLILITRE, "mL" },
        { VolumeUnitType.GALLON, "gal" }
    };

    private static readonly Dictionary<VolumeUnitType, string> UnitNames = new()
    {
        { VolumeUnitType.LITRE, "Litre" },
        { VolumeUnitType.MILLILITRE, "Millilitre" },
        { VolumeUnitType.GALLON, "Gallon" }
    };

    private VolumeUnit(VolumeUnitType unitType)
    {
        _unitType = unitType;
    }

    public static VolumeUnit Litre => new(VolumeUnitType.LITRE);
    public static VolumeUnit Millilitre => new(VolumeUnitType.MILLILITRE);
    public static VolumeUnit Gallon => new(VolumeUnitType.GALLON);

    public double GetConversionFactor() => ConversionFactors[_unitType];
    public double ConvertToBaseUnit(double value) => value * GetConversionFactor();
    public double ConvertFromBaseUnit(double baseValue) => baseValue / GetConversionFactor();
    public string GetUnitSymbol() => UnitSymbols[_unitType];
    public string GetUnitName() => UnitNames[_unitType];

    public override bool Equals(object? obj)
    {
        if (obj is not VolumeUnit other) return false;
        return _unitType == other._unitType;
    }

    public override int GetHashCode() => _unitType.GetHashCode();
}