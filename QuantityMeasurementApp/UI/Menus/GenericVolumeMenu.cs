using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using QuantityMeasurementApp.UI.Helpers;

namespace QuantityMeasurementApp.UI.Menus;

public class GenericVolumeMenu
{
    private readonly GenericMeasurementService _service;

    public GenericVolumeMenu()
    {
        _service = new GenericMeasurementService();
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteSubHeader("VOLUME MEASUREMENTS");
            Console.WriteLine("\n1. Add Volume Measurement");
            Console.WriteLine("2. Compare Volume Measurements");
            Console.WriteLine("3. Convert Volume Unit");
            Console.WriteLine("4. Add Two Volumes");
            Console.WriteLine("5. Add Two Volumes (Specify Target Unit)");
            Console.WriteLine("6. Subtract Two Volumes");
            Console.WriteLine("7. Subtract Two Volumes (Specify Target Unit)");
            Console.WriteLine("8. Divide Two Volumes (Get Ratio)");
            Console.WriteLine("9. View All Volume Measurements");
            Console.WriteLine("10. Back to Main Menu");

            int choice = ConsoleHelper.ReadInt("\nSelect option", 1, 10);

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
                    AddMeasurements();
                    break;
                case 5:
                    AddMeasurementsWithTarget();
                    break;
                case 6:
                    SubtractMeasurements();
                    break;
                case 7:
                    SubtractMeasurementsWithTarget();
                    break;
                case 8:
                    DivideMeasurements();
                    break;
                case 9:
                    ViewMeasurements();
                    ConsoleHelper.PressAnyKeyToContinue();
                    break;
                case 10:
                    return;
            }
        }
    }

    private void AddMeasurement()
    {
        ConsoleHelper.WriteSubHeader("ADD VOLUME MEASUREMENT");
        
        double value = ConsoleHelper.ReadDouble("Enter volume value");
        var unit = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");

        try
        {
            var volume = new GenericQuantity<VolumeUnit>(value, unit);
            _service.AddMeasurement(volume);
            ConsoleHelper.WriteSuccess($"Added: {volume}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void CompareMeasurements()
    {
        ConsoleHelper.WriteSubHeader("COMPARE VOLUME MEASUREMENTS");

        try
        {
            Console.WriteLine("\nFirst Volume:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume1 = new GenericQuantity<VolumeUnit>(value1, unit1);

            Console.WriteLine("\nSecond Volume:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume2 = new GenericQuantity<VolumeUnit>(value2, unit2);

            bool areEqual = _service.CompareMeasurements(volume1, volume2);
            
            Console.WriteLine($"\n{volume1} compared to {volume2}:");
            if (areEqual)
                ConsoleHelper.WriteSuccess("✓ Measurements are EQUAL");
            else
                ConsoleHelper.WriteWarning("✗ Measurements are NOT EQUAL");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ConvertMeasurement()
    {
        ConsoleHelper.WriteSubHeader("CONVERT VOLUME UNIT");

        try
        {
            double value = ConsoleHelper.ReadDouble("Enter value to convert");
            var source = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select source unit");
            var target = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select target unit");

            var quantity = new GenericQuantity<VolumeUnit>(value, source);
            var result = _service.ConvertMeasurement(quantity, target);

            ConsoleHelper.WriteSuccess($"\n{quantity} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddMeasurements()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO VOLUMES");

        try
        {
            Console.WriteLine("\nFirst Volume:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume1 = new GenericQuantity<VolumeUnit>(value1, unit1);

            Console.WriteLine("\nSecond Volume:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume2 = new GenericQuantity<VolumeUnit>(value2, unit2);

            var result = _service.AddMeasurements(volume1, volume2);

            ConsoleHelper.WriteSuccess($"\n{volume1} + {volume2} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddMeasurementsWithTarget()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO VOLUMES (WITH TARGET UNIT)");

        try
        {
            Console.WriteLine("\nFirst Volume:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume1 = new GenericQuantity<VolumeUnit>(value1, unit1);

            Console.WriteLine("\nSecond Volume:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume2 = new GenericQuantity<VolumeUnit>(value2, unit2);

            Console.WriteLine("\nTarget Unit:");
            var target = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select target unit for result");

            var result = _service.AddMeasurementsWithTarget(volume1, volume2, target);

            ConsoleHelper.WriteSuccess($"\n{volume1} + {volume2} = {result} (in {target.GetUnitSymbol()})");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void SubtractMeasurements()
    {
        ConsoleHelper.WriteSubHeader("SUBTRACT TWO VOLUMES");

        try
        {
            Console.WriteLine("\nFirst Volume:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume1 = new GenericQuantity<VolumeUnit>(value1, unit1);

            Console.WriteLine("\nSecond Volume (to subtract):");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume2 = new GenericQuantity<VolumeUnit>(value2, unit2);

            var result = _service.SubtractMeasurements(volume1, volume2);

            ConsoleHelper.WriteSuccess($"\n{volume1} - {volume2} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void SubtractMeasurementsWithTarget()
    {
        ConsoleHelper.WriteSubHeader("SUBTRACT TWO VOLUMES (WITH TARGET UNIT)");

        try
        {
            Console.WriteLine("\nFirst Volume:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume1 = new GenericQuantity<VolumeUnit>(value1, unit1);

            Console.WriteLine("\nSecond Volume (to subtract):");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume2 = new GenericQuantity<VolumeUnit>(value2, unit2);

            Console.WriteLine("\nTarget Unit:");
            var target = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select target unit for result");

            var result = _service.SubtractMeasurementsWithTarget(volume1, volume2, target);

            ConsoleHelper.WriteSuccess($"\n{volume1} - {volume2} = {result} (in {target.GetUnitSymbol()})");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void DivideMeasurements()
    {
        ConsoleHelper.WriteSubHeader("DIVIDE TWO VOLUMES (GET RATIO)");

        try
        {
            Console.WriteLine("\nFirst Volume (dividend):");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume1 = new GenericQuantity<VolumeUnit>(value1, unit1);

            Console.WriteLine("\nSecond Volume (divisor):");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (VolumeUnit)GenericUnitSelector.SelectVolumeUnit("Select unit");
            var volume2 = new GenericQuantity<VolumeUnit>(value2, unit2);

            double ratio = _service.DivideMeasurements(volume1, volume2);

            ConsoleHelper.WriteSuccess($"\n{volume1} ÷ {volume2} = {ratio:F4}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    public void ViewMeasurements()
    {
        var measurements = _service.GetMeasurementsByType<VolumeUnit>();
        if (!measurements.Any())
        {
            ConsoleHelper.WriteInfo("  No volume measurements found.");
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