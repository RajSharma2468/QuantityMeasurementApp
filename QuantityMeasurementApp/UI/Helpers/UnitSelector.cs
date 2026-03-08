using System;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Core.Helpers;

public static class UnitSelector
{
    public static WeightUnit SelectWeightUnit(string prompt)
    {
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("1. Kilogram (kg)");
        Console.WriteLine("2. Gram (g)");
        Console.WriteLine("3. Pound (lb)");

        int choice = ConsoleHelper.ReadInt("Select unit", 1, 3);

        return choice switch
        {
            1 => WeightUnit.KILOGRAM,
            2 => WeightUnit.GRAM,
            3 => WeightUnit.POUND,
            _ => WeightUnit.KILOGRAM
        };
    }

    public static LengthUnit SelectLengthUnit(string prompt)
    {
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("1. Inch (in)");
        Console.WriteLine("2. Feet (ft)");
        Console.WriteLine("3. Yard (yd)");
        Console.WriteLine("4. Centimeter (cm)");

        int choice = ConsoleHelper.ReadInt("Select unit", 1, 4);

        return choice switch
        {
            1 => LengthUnit.INCH,
            2 => LengthUnit.FEET,
            3 => LengthUnit.YARD,
            4 => LengthUnit.CENTIMETER,
            _ => LengthUnit.FEET
        };
    }
}