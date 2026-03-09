using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using QuantityMeasurementApp.UI.Helpers;

namespace QuantityMeasurementApp.UI.Menus;

public class GenericWeightMenu
{
    private readonly GenericMeasurementService _service;

    public GenericWeightMenu()
    {
        _service = new GenericMeasurementService();
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteSubHeader("WEIGHT MEASUREMENTS");
            Console.WriteLine("\n1. Add Weight Measurement");
            Console.WriteLine("2. Compare Weight Measurements");
            Console.WriteLine("3. Convert Weight Unit");
            Console.WriteLine("4. Add Two Weights");
            Console.WriteLine("5. Add Two Weights (Specify Target Unit)");
            Console.WriteLine("6. View All Weight Measurements");
            Console.WriteLine("7. Back to Main Menu");

            int choice = ConsoleHelper.ReadInt("\nSelect option", 1, 7);

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
                    ViewMeasurements();
                    ConsoleHelper.PressAnyKeyToContinue();
                    break;
                case 7:
                    return;
            }
        }
    }

    private void AddMeasurement()
    {
        ConsoleHelper.WriteSubHeader("ADD WEIGHT MEASUREMENT");
        
        double value = ConsoleHelper.ReadDouble("Enter weight value");
        var unit = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select unit");

        try
        {
            var weight = new GenericQuantity<WeightUnit>(value, unit);
            _service.AddMeasurement(weight);
            ConsoleHelper.WriteSuccess($"Added: {weight}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void CompareMeasurements()
    {
        ConsoleHelper.WriteSubHeader("COMPARE WEIGHT MEASUREMENTS");

        try
        {
            Console.WriteLine("\nFirst Weight:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select unit");
            var weight1 = new GenericQuantity<WeightUnit>(value1, unit1);

            Console.WriteLine("\nSecond Weight:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select unit");
            var weight2 = new GenericQuantity<WeightUnit>(value2, unit2);

            bool areEqual = _service.CompareMeasurements(weight1, weight2);
            
            Console.WriteLine($"\n{weight1} compared to {weight2}:");
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
        ConsoleHelper.WriteSubHeader("CONVERT WEIGHT UNIT");

        try
        {
            double value = ConsoleHelper.ReadDouble("Enter value to convert");
            var source = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select source unit");
            var target = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select target unit");

            var quantity = new GenericQuantity<WeightUnit>(value, source);
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
        ConsoleHelper.WriteSubHeader("ADD TWO WEIGHTS");

        try
        {
            Console.WriteLine("\nFirst Weight:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select unit");
            var weight1 = new GenericQuantity<WeightUnit>(value1, unit1);

            Console.WriteLine("\nSecond Weight:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select unit");
            var weight2 = new GenericQuantity<WeightUnit>(value2, unit2);

            var result = _service.AddMeasurements(weight1, weight2);

            ConsoleHelper.WriteSuccess($"\n{weight1} + {weight2} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddMeasurementsWithTarget()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO WEIGHTS (WITH TARGET UNIT)");

        try
        {
            Console.WriteLine("\nFirst Weight:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select unit");
            var weight1 = new GenericQuantity<WeightUnit>(value1, unit1);

            Console.WriteLine("\nSecond Weight:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select unit");
            var weight2 = new GenericQuantity<WeightUnit>(value2, unit2);

            Console.WriteLine("\nTarget Unit:");
            var target = (WeightUnit)GenericUnitSelector.SelectWeightUnit("Select target unit for result");

            var result = _service.AddMeasurementsWithTarget(weight1, weight2, target);

            ConsoleHelper.WriteSuccess($"\n{weight1} + {weight2} = {result} (in {target.GetUnitSymbol()})");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    public void ViewMeasurements()
    {
        var measurements = _service.GetMeasurementsByType<WeightUnit>();
        if (!measurements.Any())
        {
            ConsoleHelper.WriteInfo("  No weight measurements found.");
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