using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.TestHelpers;

public static class TestDataFactory
{
    public static GenericQuantity<LengthUnit> CreateLength(double value, string unit)
    {
        var u = unit.ToLower() switch
        {
            "inch" => LengthUnit.Inch,
            "feet" => LengthUnit.Feet,
            "yard" => LengthUnit.Yard,
            "centimeter" => LengthUnit.Centimeter,
            _ => LengthUnit.Feet
        };
        return new GenericQuantity<LengthUnit>(value, u);
    }

    public static GenericQuantity<WeightUnit> CreateWeight(double value, string unit)
    {
        var u = unit.ToLower() switch
        {
            "kilogram" => WeightUnit.Kilogram,
            "gram" => WeightUnit.Gram,
            "pound" => WeightUnit.Pound,
            _ => WeightUnit.Kilogram
        };
        return new GenericQuantity<WeightUnit>(value, u);
    }

    public static GenericQuantity<VolumeUnit> CreateVolume(double value, string unit)
    {
        var u = unit.ToLower() switch
        {
            "litre" => VolumeUnit.Litre,
            "millilitre" => VolumeUnit.Millilitre,
            "gallon" => VolumeUnit.Gallon,
            _ => VolumeUnit.Litre
        };
        return new GenericQuantity<VolumeUnit>(value, u);
    }

    public static GenericQuantity<TemperatureUnit> CreateTemperature(double value, string unit)
    {
        var u = unit.ToLower() switch
        {
            "celsius" => TemperatureUnit.Celsius,
            "fahrenheit" => TemperatureUnit.Fahrenheit,
            "kelvin" => TemperatureUnit.Kelvin,
            _ => TemperatureUnit.Celsius
        };
        return new GenericQuantity<TemperatureUnit>(value, u);
    }
}