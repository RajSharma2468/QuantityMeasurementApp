using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.UI.Helpers;

public static class TemperatureUnitSelector
{
    public static TemperatureUnit SelectUnit(string prompt)
    {
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("1. Celsius (°C)");
        Console.WriteLine("2. Fahrenheit (°F)");
        Console.WriteLine("3. Kelvin (K)");

        int choice = ConsoleHelper.ReadInt("Select unit", 1, 3);

        return choice switch
        {
            1 => TemperatureUnit.Celsius,
            2 => TemperatureUnit.Fahrenheit,
            3 => TemperatureUnit.Kelvin,
            _ => TemperatureUnit.Celsius
        };
    }
}