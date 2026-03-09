using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Utils.Validators;

public static class UnitValidator
{
    public static bool IsValidLengthUnit(LengthUnit? unit)
    {
        if (unit == null) return false;
        
        return unit.Equals(LengthUnit.Inch) ||
               unit.Equals(LengthUnit.Feet) ||
               unit.Equals(LengthUnit.Yard) ||
               unit.Equals(LengthUnit.Centimeter);
    }

    public static bool IsValidWeightUnit(WeightUnit? unit)
    {
        if (unit == null) return false;
        
        return unit.Equals(WeightUnit.Kilogram) ||
               unit.Equals(WeightUnit.Gram) ||
               unit.Equals(WeightUnit.Pound);
    }

    public static bool IsValidVolumeUnit(VolumeUnit? unit)
    {
        if (unit == null) return false;
        
        return unit.Equals(VolumeUnit.Litre) ||
               unit.Equals(VolumeUnit.Millilitre) ||
               unit.Equals(VolumeUnit.Gallon);
    }

    public static bool IsValidMeasurableUnit(IMeasurable? unit)
    {
        return unit != null;
    }

    public static bool AreSameCategory(IMeasurable unit1, IMeasurable unit2)
    {
        if (unit1 == null || unit2 == null) return false;
        
        return unit1.GetType() == unit2.GetType();
    }

    public static void ValidateSameCategory(IMeasurable unit1, IMeasurable unit2)
    {
        if (!AreSameCategory(unit1, unit2))
            throw new InvalidOperationException($"Cannot operate on different measurement categories: {unit1.GetType().Name} and {unit2.GetType().Name}");
    }

    public static List<LengthUnit> GetAllLengthUnits()
    {
        return new List<LengthUnit>
        {
            LengthUnit.Inch,
            LengthUnit.Feet,
            LengthUnit.Yard,
            LengthUnit.Centimeter
        };
    }

    public static List<WeightUnit> GetAllWeightUnits()
    {
        return new List<WeightUnit>
        {
            WeightUnit.Kilogram,
            WeightUnit.Gram,
            WeightUnit.Pound
        };
    }

    public static List<VolumeUnit> GetAllVolumeUnits()
    {
        return new List<VolumeUnit>
        {
            VolumeUnit.Litre,
            VolumeUnit.Millilitre,
            VolumeUnit.Gallon
        };
    }
}