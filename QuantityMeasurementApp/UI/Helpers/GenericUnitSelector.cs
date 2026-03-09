using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.UI.Helpers;

public static class GenericUnitSelector
{
    public static IMeasurable SelectVolumeUnit(string prompt)
    {
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("1. Litre (L)");
        Console.WriteLine("2. Millilitre (mL)");
        Console.WriteLine("3. Gallon (gal)");

        int choice = ConsoleHelper.ReadInt("Select unit", 1, 3);

        return choice switch
        {
            1 => VolumeUnit.Litre,
            2 => VolumeUnit.Millilitre,
            3 => VolumeUnit.Gallon,
            _ => VolumeUnit.Litre
        };
    }

    public static IMeasurable SelectWeightUnit(string prompt)
    {
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("1. Kilogram (kg)");
        Console.WriteLine("2. Gram (g)");
        Console.WriteLine("3. Pound (lb)");

        int choice = ConsoleHelper.ReadInt("Select unit", 1, 3);

        return choice switch
        {
            1 => WeightUnit.Kilogram,
            2 => WeightUnit.Gram,
            3 => WeightUnit.Pound,
            _ => WeightUnit.Kilogram
        };
    }

    public static IMeasurable SelectLengthUnit(string prompt)
    {
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("1. Inch (in)");
        Console.WriteLine("2. Feet (ft)");
        Console.WriteLine("3. Yard (yd)");
        Console.WriteLine("4. Centimeter (cm)");

        int choice = ConsoleHelper.ReadInt("Select unit", 1, 4);

        return choice switch
        {
            1 => LengthUnit.Inch,
            2 => LengthUnit.Feet,
            3 => LengthUnit.Yard,
            4 => LengthUnit.Centimeter,
            _ => LengthUnit.Feet
        };
    }

    public static IMeasurable SelectUnitByCategory(string category, string prompt)
    {
        return category.ToLower() switch
        {
            "volume" => SelectVolumeUnit(prompt),
            "weight" => SelectWeightUnit(prompt),
            "length" => SelectLengthUnit(prompt),
            _ => throw new ArgumentException($"Unknown category: {category}")
        };
    }
}