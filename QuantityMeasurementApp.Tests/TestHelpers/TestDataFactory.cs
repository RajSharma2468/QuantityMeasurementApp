using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.TestHelpers;

public static class TestDataFactory
{
    public static GenericQuantity<LengthUnit> CreateLengthQuantity(double value, string unit)
    {
        var lengthUnit = unit.ToLower() switch
        {
            "inch" => LengthUnit.Inch,
            "feet" => LengthUnit.Feet,
            "yard" => LengthUnit.Yard,
            "centimeter" => LengthUnit.Centimeter,
            _ => LengthUnit.Feet
        };
        
        return new GenericQuantity<LengthUnit>(value, lengthUnit);
    }

    public static GenericQuantity<WeightUnit> CreateWeightQuantity(double value, string unit)
    {
        var weightUnit = unit.ToLower() switch
        {
            "kilogram" => WeightUnit.Kilogram,
            "gram" => WeightUnit.Gram,
            "pound" => WeightUnit.Pound,
            _ => WeightUnit.Kilogram
        };
        
        return new GenericQuantity<WeightUnit>(value, weightUnit);
    }

    public static List<GenericQuantity<LengthUnit>> GetSampleLengthMeasurements()
    {
        return new List<GenericQuantity<LengthUnit>>
        {
            new(10, LengthUnit.Feet),
            new(5, LengthUnit.Yard),
            new(120, LengthUnit.Inch),
            new(304.8, LengthUnit.Centimeter)
        };
    }

    public static List<GenericQuantity<WeightUnit>> GetSampleWeightMeasurements()
    {
        return new List<GenericQuantity<WeightUnit>>
        {
            new(10, WeightUnit.Kilogram),
            new(5000, WeightUnit.Gram),
            new(22.0462, WeightUnit.Pound)
        };
    }
}