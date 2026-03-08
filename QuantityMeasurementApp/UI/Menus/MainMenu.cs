using System;
using System.Linq;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Helpers;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.UI.Menus;

public class MainMenu
{
    private readonly QuantityMeasurementService _service;
    private readonly ConversionMenu _conversionMenu;

    public MainMenu()
    {
        _service = new QuantityMeasurementService();
        _conversionMenu = new ConversionMenu(_service);
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteHeader("QUANTITY MEASUREMENT APPLICATION");
            Console.WriteLine("\n1. Weight Measurements");
            Console.WriteLine("2. Length Measurements");
            Console.WriteLine("3. View All Measurements");
            Console.WriteLine("4. Clear All Measurements");
            Console.WriteLine("5. Exit");

            int choice = ConsoleHelper.ReadInt("\nSelect option", 1, 5);

            switch (choice)
            {
                case 1:
                    RunWeightMenu();
                    break;
                case 2:
                    RunLengthMenu();
                    break;
                case 3:
                    ViewAllMeasurements();
                    break;
                case 4:
                    ClearAllMeasurements();
                    break;
                case 5:
                    ConsoleHelper.WriteInfo("\nThank you for using the application!");
                    return;
            }
        }
    }

    private void RunWeightMenu()
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
                    AddWeightMeasurement();
                    break;
                case 2:
                    CompareWeightMeasurements();
                    break;
                case 3:
                    ConvertWeight();
                    break;
                case 4:
                    AddWeights();
                    break;
                case 5:
                    AddWeightsWithTarget();
                    break;
                case 6:
                    ViewWeightMeasurements();
                    break;
                case 7:
                    return;
            }
        }
    }

    private void RunLengthMenu()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteSubHeader("LENGTH MEASUREMENTS");
            Console.WriteLine("\n1. Add Length Measurement");
            Console.WriteLine("2. Compare Length Measurements");
            Console.WriteLine("3. Convert Length Unit");
            Console.WriteLine("4. Add Two Lengths");
            Console.WriteLine("5. Add Two Lengths (Specify Target Unit)");
            Console.WriteLine("6. View All Length Measurements");
            Console.WriteLine("7. Back to Main Menu");

            int choice = ConsoleHelper.ReadInt("\nSelect option", 1, 7);

            switch (choice)
            {
                case 1:
                    AddLengthMeasurement();
                    break;
                case 2:
                    CompareLengthMeasurements();
                    break;
                case 3:
                    ConvertLength();
                    break;
                case 4:
                    AddLengths();
                    break;
                case 5:
                    AddLengthsWithTarget();
                    break;
                case 6:
                    ViewLengthMeasurements();
                    break;
                case 7:
                    return;
            }
        }
    }

    private void AddWeightMeasurement()
    {
        ConsoleHelper.WriteSubHeader("ADD WEIGHT MEASUREMENT");
        
        double value = ConsoleHelper.ReadDouble("Enter weight value");
        WeightUnit unit = UnitSelector.SelectWeightUnit("Select unit");

        try
        {
            var weight = new QuantityWeight(value, unit);
            _service.AddWeightMeasurement(weight);
            ConsoleHelper.WriteSuccess($"Added: {weight}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddLengthMeasurement()
    {
        ConsoleHelper.WriteSubHeader("ADD LENGTH MEASUREMENT");
        
        double value = ConsoleHelper.ReadDouble("Enter length value");
        LengthUnit unit = UnitSelector.SelectLengthUnit("Select unit");

        try
        {
            var length = new QuantityLength(value, unit);
            _service.AddLengthMeasurement(length);
            ConsoleHelper.WriteSuccess($"Added: {length}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void CompareWeightMeasurements()
    {
        ConsoleHelper.WriteSubHeader("COMPARE WEIGHT MEASUREMENTS");

        try
        {
            Console.WriteLine("\nFirst Weight:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            WeightUnit unit1 = UnitSelector.SelectWeightUnit("Select unit");
            var weight1 = new QuantityWeight(value1, unit1);

            Console.WriteLine("\nSecond Weight:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            WeightUnit unit2 = UnitSelector.SelectWeightUnit("Select unit");
            var weight2 = new QuantityWeight(value2, unit2);

            bool areEqual = _service.CompareWeightMeasurements(weight1, weight2);
            
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

    private void CompareLengthMeasurements()
    {
        ConsoleHelper.WriteSubHeader("COMPARE LENGTH MEASUREMENTS");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            LengthUnit unit1 = UnitSelector.SelectLengthUnit("Select unit");
            var length1 = new QuantityLength(value1, unit1);

            Console.WriteLine("\nSecond Length:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            LengthUnit unit2 = UnitSelector.SelectLengthUnit("Select unit");
            var length2 = new QuantityLength(value2, unit2);

            bool areEqual = _service.CompareLengthMeasurements(length1, length2);
            
            Console.WriteLine($"\n{length1} compared to {length2}:");
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

    private void ConvertWeight()
    {
        ConsoleHelper.WriteSubHeader("CONVERT WEIGHT UNIT");

        try
        {
            double value = ConsoleHelper.ReadDouble("Enter value to convert");
            WeightUnit sourceUnit = UnitSelector.SelectWeightUnit("Select source unit");
            WeightUnit targetUnit = UnitSelector.SelectWeightUnit("Select target unit");

            var source = new QuantityWeight(value, sourceUnit);
            var result = _service.ConvertWeight(source, targetUnit);

            ConsoleHelper.WriteSuccess($"\n{source} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ConvertLength()
    {
        ConsoleHelper.WriteSubHeader("CONVERT LENGTH UNIT");

        try
        {
            double value = ConsoleHelper.ReadDouble("Enter value to convert");
            LengthUnit sourceUnit = UnitSelector.SelectLengthUnit("Select source unit");
            LengthUnit targetUnit = UnitSelector.SelectLengthUnit("Select target unit");

            var source = new QuantityLength(value, sourceUnit);
            var result = _service.ConvertLength(source, targetUnit);

            ConsoleHelper.WriteSuccess($"\n{source} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddWeights()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO WEIGHTS");

        try
        {
            Console.WriteLine("\nFirst Weight:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            WeightUnit unit1 = UnitSelector.SelectWeightUnit("Select unit");
            var weight1 = new QuantityWeight(value1, unit1);

            Console.WriteLine("\nSecond Weight:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            WeightUnit unit2 = UnitSelector.SelectWeightUnit("Select unit");
            var weight2 = new QuantityWeight(value2, unit2);

            var result = _service.AddWeightMeasurements(weight1, weight2);

            ConsoleHelper.WriteSuccess($"\n{weight1} + {weight2} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddLengths()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO LENGTHS");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            LengthUnit unit1 = UnitSelector.SelectLengthUnit("Select unit");
            var length1 = new QuantityLength(value1, unit1);

            Console.WriteLine("\nSecond Length:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            LengthUnit unit2 = UnitSelector.SelectLengthUnit("Select unit");
            var length2 = new QuantityLength(value2, unit2);

            var result = _service.AddLengthMeasurements(length1, length2);

            ConsoleHelper.WriteSuccess($"\n{length1} + {length2} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddWeightsWithTarget()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO WEIGHTS (WITH TARGET UNIT)");

        try
        {
            Console.WriteLine("\nFirst Weight:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            WeightUnit unit1 = UnitSelector.SelectWeightUnit("Select unit");
            var weight1 = new QuantityWeight(value1, unit1);

            Console.WriteLine("\nSecond Weight:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            WeightUnit unit2 = UnitSelector.SelectWeightUnit("Select unit");
            var weight2 = new QuantityWeight(value2, unit2);

            Console.WriteLine("\nTarget Unit:");
            WeightUnit targetUnit = UnitSelector.SelectWeightUnit("Select target unit for result");

            var result = _service.AddWeightMeasurementsWithTarget(weight1, weight2, targetUnit);

            ConsoleHelper.WriteSuccess($"\n{weight1} + {weight2} = {result} (in {targetUnit.GetUnitSymbol()})");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddLengthsWithTarget()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO LENGTHS (WITH TARGET UNIT)");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            LengthUnit unit1 = UnitSelector.SelectLengthUnit("Select unit");
            var length1 = new QuantityLength(value1, unit1);

            Console.WriteLine("\nSecond Length:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            LengthUnit unit2 = UnitSelector.SelectLengthUnit("Select unit");
            var length2 = new QuantityLength(value2, unit2);

            Console.WriteLine("\nTarget Unit:");
            LengthUnit targetUnit = UnitSelector.SelectLengthUnit("Select target unit for result");

            var result = _service.AddLengthMeasurementsWithTarget(length1, length2, targetUnit);

            ConsoleHelper.WriteSuccess($"\n{length1} + {length2} = {result} (in {targetUnit.GetUnitSymbol()})");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ViewWeightMeasurements()
    {
        ConsoleHelper.WriteSubHeader("WEIGHT MEASUREMENTS");
        
        var measurements = _service.GetAllWeightMeasurements();
        if (!measurements.Any())
        {
            ConsoleHelper.WriteInfo("No weight measurements found.");
        }
        else
        {
            for (int i = 0; i < measurements.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {measurements[i]}");
            }
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ViewLengthMeasurements()
    {
        ConsoleHelper.WriteSubHeader("LENGTH MEASUREMENTS");
        
        var measurements = _service.GetAllLengthMeasurements();
        if (!measurements.Any())
        {
            ConsoleHelper.WriteInfo("No length measurements found.");
        }
        else
        {
            for (int i = 0; i < measurements.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {measurements[i]}");
            }
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ViewAllMeasurements()
    {
        ConsoleHelper.WriteSubHeader("ALL MEASUREMENTS");

        Console.WriteLine("\nWEIGHT MEASUREMENTS:");
        var weightMeasurements = _service.GetAllWeightMeasurements();
        if (!weightMeasurements.Any())
        {
            ConsoleHelper.WriteInfo("  No weight measurements found.");
        }
        else
        {
            foreach (var m in weightMeasurements)
            {
                Console.WriteLine($"  • {m}");
            }
        }

        Console.WriteLine("\nLENGTH MEASUREMENTS:");
        var lengthMeasurements = _service.GetAllLengthMeasurements();
        if (!lengthMeasurements.Any())
        {
            ConsoleHelper.WriteInfo("  No length measurements found.");
        }
        else
        {
            foreach (var m in lengthMeasurements)
            {
                Console.WriteLine($"  • {m}");
            }
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ClearAllMeasurements()
    {
        if (ConsoleHelper.ReadConfirmation("Are you sure you want to clear all measurements?"))
        {
            _service.ClearAllMeasurements();
            ConsoleHelper.WriteSuccess("All measurements cleared successfully.");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }
}