namespace QuantityMeasurementModelLayer.Entities;

public static class UnitConversionFactor
{
    // Length conversions (base unit: Inch)
    public static readonly Dictionary<string, double> LengthToInch = new()
    {
        ["Inch"] = 1,
        ["Feet"] = 12,
        ["Yard"] = 36,
        ["Centimeter"] = 0.393701,
        ["Meter"] = 39.3701
    };
    
    // Weight conversions (base unit: Gram)
    public static readonly Dictionary<string, double> WeightToGram = new()
    {
        ["Gram"] = 1,
        ["Kilogram"] = 1000,
        ["Pound"] = 453.592,
        ["Ounce"] = 28.3495
    };
    
    // Volume conversions (base unit: Milliliter)
    public static readonly Dictionary<string, double> VolumeToMilliliter = new()
    {
        ["Milliliter"] = 1,
        ["Liter"] = 1000,
        ["Gallon"] = 3785.41,
        ["Cup"] = 236.588
    };
}