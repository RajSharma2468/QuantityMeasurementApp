using System;

namespace QuantityMeasurementApp.Core.Domain.Units;

public enum LengthUnit
{
    INCH,
    FEET,
    YARD,
    CENTIMETER
}

public static class LengthUnitExtensions
{
    private const double INCH_TO_FEET = 1.0 / 12.0;
    private const double YARD_TO_FEET = 3.0;
    private const double CENTIMETER_TO_FEET = 1.0 / 30.48;

    public static double GetConversionFactorToFeet(this LengthUnit unit)
    {
        return unit switch
        {
            LengthUnit.INCH => INCH_TO_FEET,
            LengthUnit.FEET => 1.0,
            LengthUnit.YARD => YARD_TO_FEET,
            LengthUnit.CENTIMETER => CENTIMETER_TO_FEET,
            _ => throw new ArgumentException($"Unknown length unit: {unit}")
        };
    }

    public static double ConvertToBaseUnit(this LengthUnit unit, double value)
    {
        return value * unit.GetConversionFactorToFeet();
    }

    public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
    {
        return baseValue / unit.GetConversionFactorToFeet();
    }

    public static string GetUnitSymbol(this LengthUnit unit)
    {
        return unit switch
        {
            LengthUnit.INCH => "in",
            LengthUnit.FEET => "ft",
            LengthUnit.YARD => "yd",
            LengthUnit.CENTIMETER => "cm",
            _ => unit.ToString()
        };
    }
}