using System;

namespace QuantityMeasurementApp.Core.Domain.Units;

public enum WeightUnit
{
    KILOGRAM,
    GRAM,
    POUND
}

public static class WeightUnitExtensions
{
    private const double GRAM_TO_KG = 0.001;
    private const double POUND_TO_KG = 0.453592;

    public static double GetConversionFactorToKg(this WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.KILOGRAM => 1.0,
            WeightUnit.GRAM => GRAM_TO_KG,
            WeightUnit.POUND => POUND_TO_KG,
            _ => throw new ArgumentException($"Unknown weight unit: {unit}")
        };
    }

    public static double ConvertToBaseUnit(this WeightUnit unit, double value)
    {
        return value * unit.GetConversionFactorToKg();
    }

    public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
    {
        return baseValue / unit.GetConversionFactorToKg();
    }

    public static string GetUnitSymbol(this WeightUnit unit)
    {
        return unit switch
        {
            WeightUnit.KILOGRAM => "kg",
            WeightUnit.GRAM => "g",
            WeightUnit.POUND => "lb",
            _ => unit.ToString()
        };
    }
}