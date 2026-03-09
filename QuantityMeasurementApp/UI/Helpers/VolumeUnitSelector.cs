using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.UI.Helpers;

public static class VolumeUnitSelector
{
    public static VolumeUnit SelectUnit(string prompt)
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
}