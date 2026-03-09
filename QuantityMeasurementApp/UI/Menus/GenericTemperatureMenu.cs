using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using QuantityMeasurementApp.Core.Exceptions;
using QuantityMeasurementApp.UI.Helpers;

namespace QuantityMeasurementApp.UI.Menus;

public class GenericTemperatureMenu
{
    private readonly GenericMeasurementService _service;

    public GenericTemperatureMenu()
    {
        _service = new GenericMeasurementService();
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteSubHeader("TEMPERATURE MEASUREMENTS");
            Console.WriteLine("\n1. Add Temperature Measurement");
            Console.WriteLine("2. Compare Temperature Measurements");
            Console.WriteLine("3. Convert Temperature Unit");
            Console.WriteLine("4. Try Add Two Temperatures (Will Fail - Demo)");
            Console.WriteLine("5. Try Subtract Two Temperatures (Will Fail - Demo)");
            Console.WriteLine("6. Try Divide Two Temperatures (Will Fail - Demo)");
            Console.WriteLine("7. View All Temperature Measurements");
            Console.WriteLine("8. Back to Main Menu");

            int choice = ConsoleHelper.ReadInt("\nSelect option", 1, 8);

            switch (choice)
            {
                case 1:
                    AddMeasurement();
                    break;
                case 2:
                    CompareMeasurements();
                    break;
                case 3:
                    ConvertMeasurement();
                    break;
                case 4:
                    TryAddMeasurements();
                    break;
                case 5:
                    TrySubtractMeasurements();
                    break;
                case 6:
                    TryDivideMeasurements();
                    break;
                case 7:
                    ViewMeasurements();
                    ConsoleHelper.PressAnyKeyToContinue();
                    break;
                case 8:
                    return;
            }
        }
    }

    private void AddMeasurement()
    {
        ConsoleHelper.WriteSubHeader("ADD TEMPERATURE MEASUREMENT");
        
        double value = ConsoleHelper.ReadDouble("Enter temperature value");
        var unit = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");

        try
        {
            var temperature = new GenericQuantity<TemperatureUnit>(value, unit);
            _service.AddMeasurement(temperature);
            ConsoleHelper.WriteSuccess($"Added: {temperature}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void CompareMeasurements()
    {
        ConsoleHelper.WriteSubHeader("COMPARE TEMPERATURE MEASUREMENTS");

        try
        {
            Console.WriteLine("\nFirst Temperature:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp1 = new GenericQuantity<TemperatureUnit>(value1, unit1);

            Console.WriteLine("\nSecond Temperature:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp2 = new GenericQuantity<TemperatureUnit>(value2, unit2);

            bool areEqual = _service.CompareMeasurements(temp1, temp2);
            
            Console.WriteLine($"\n{temp1} compared to {temp2}:");
            if (areEqual)
                ConsoleHelper.WriteSuccess("✓ Temperatures are EQUAL");
            else
                ConsoleHelper.WriteWarning("✗ Temperatures are NOT EQUAL");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ConvertMeasurement()
    {
        ConsoleHelper.WriteSubHeader("CONVERT TEMPERATURE UNIT");

        try
        {
            double value = ConsoleHelper.ReadDouble("Enter value to convert");
            var source = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select source unit");
            var target = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select target unit");

            var quantity = new GenericQuantity<TemperatureUnit>(value, source);
            var result = _service.ConvertMeasurement(quantity, target);

            ConsoleHelper.WriteSuccess($"\n{quantity} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void TryAddMeasurements()
    {
        ConsoleHelper.WriteSubHeader("TRY ADD TWO TEMPERATURES (DEMO)");
        ConsoleHelper.WriteInfo("Temperature does not support addition operations.");
        ConsoleHelper.WriteInfo("This will demonstrate the error handling.");

        try
        {
            Console.WriteLine("\nFirst Temperature:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp1 = new GenericQuantity<TemperatureUnit>(value1, unit1);

            Console.WriteLine("\nSecond Temperature:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp2 = new GenericQuantity<TemperatureUnit>(value2, unit2);

            Console.WriteLine("\nAttempting to add...");
            var result = temp1.Add(temp2);
            
            // This line will not execute if exception is thrown
            ConsoleHelper.WriteSuccess($"Result: {result}");
        }
        catch (UnsupportedOperationException ex)
        {
            ConsoleHelper.WriteWarning($"Expected Error: {ex.Message}");
            ConsoleHelper.WriteInfo("✓ Temperature addition correctly throws UnsupportedOperationException");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Unexpected Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void TrySubtractMeasurements()
    {
        ConsoleHelper.WriteSubHeader("TRY SUBTRACT TWO TEMPERATURES (DEMO)");
        ConsoleHelper.WriteInfo("Temperature does not support subtraction operations.");
        ConsoleHelper.WriteInfo("This will demonstrate the error handling.");

        try
        {
            Console.WriteLine("\nFirst Temperature:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp1 = new GenericQuantity<TemperatureUnit>(value1, unit1);

            Console.WriteLine("\nSecond Temperature:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp2 = new GenericQuantity<TemperatureUnit>(value2, unit2);

            Console.WriteLine("\nAttempting to subtract...");
            var result = temp1.Subtract(temp2);
            
            // This line will not execute if exception is thrown
            ConsoleHelper.WriteSuccess($"Result: {result}");
        }
        catch (UnsupportedOperationException ex)
        {
            ConsoleHelper.WriteWarning($"Expected Error: {ex.Message}");
            ConsoleHelper.WriteInfo("✓ Temperature subtraction correctly throws UnsupportedOperationException");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Unexpected Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void TryDivideMeasurements()
    {
        ConsoleHelper.WriteSubHeader("TRY DIVIDE TWO TEMPERATURES (DEMO)");
        ConsoleHelper.WriteInfo("Temperature does not support division operations.");
        ConsoleHelper.WriteInfo("This will demonstrate the error handling.");

        try
        {
            Console.WriteLine("\nFirst Temperature (dividend):");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp1 = new GenericQuantity<TemperatureUnit>(value1, unit1);

            Console.WriteLine("\nSecond Temperature (divisor):");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (TemperatureUnit)GenericUnitSelector.SelectTemperatureUnit("Select unit");
            var temp2 = new GenericQuantity<TemperatureUnit>(value2, unit2);

            Console.WriteLine("\nAttempting to divide...");
            double ratio = temp1.Divide(temp2);
            
            // This line will not execute if exception is thrown
            ConsoleHelper.WriteSuccess($"Ratio: {ratio}");
        }
        catch (UnsupportedOperationException ex)
        {
            ConsoleHelper.WriteWarning($"Expected Error: {ex.Message}");
            ConsoleHelper.WriteInfo("✓ Temperature division correctly throws UnsupportedOperationException");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Unexpected Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    public void ViewMeasurements()
    {
        var measurements = _service.GetMeasurementsByType<TemperatureUnit>();
        if (!measurements.Any())
        {
            ConsoleHelper.WriteInfo("  No temperature measurements found.");
        }
        else
        {
            foreach (var m in measurements)
            {
                Console.WriteLine($"  • {m}");
            }
        }
    }

    public void ClearMeasurements()
    {
        _service.ClearAllMeasurements();
    }
}