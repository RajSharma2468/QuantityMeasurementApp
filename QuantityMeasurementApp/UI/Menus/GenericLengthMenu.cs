using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using QuantityMeasurementApp.UI.Helpers;

namespace QuantityMeasurementApp.UI.Menus;

public class GenericLengthMenu
{
    private readonly GenericMeasurementService _service;

    public GenericLengthMenu()
    {
        _service = new GenericMeasurementService();
    }

    public void Run()
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
            Console.WriteLine("6. Subtract Two Lengths");
            Console.WriteLine("7. Subtract Two Lengths (Specify Target Unit)");
            Console.WriteLine("8. Divide Two Lengths (Get Ratio)");
            Console.WriteLine("9. View All Length Measurements");
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
        ConsoleHelper.WriteSubHeader("ADD LENGTH MEASUREMENT");
        
        double value = ConsoleHelper.ReadDouble("Enter length value");
        var unit = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");

        try
        {
            var length = new GenericQuantity<LengthUnit>(value, unit);
            _service.AddMeasurement(length);
            ConsoleHelper.WriteSuccess($"Added: {length}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void CompareMeasurements()
    {
        ConsoleHelper.WriteSubHeader("COMPARE LENGTH MEASUREMENTS");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length1 = new GenericQuantity<LengthUnit>(value1, unit1);

            Console.WriteLine("\nSecond Length:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length2 = new GenericQuantity<LengthUnit>(value2, unit2);

            bool areEqual = _service.CompareMeasurements(length1, length2);
            
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

    private void ConvertMeasurement()
    {
        ConsoleHelper.WriteSubHeader("CONVERT LENGTH UNIT");

        try
        {
            double value = ConsoleHelper.ReadDouble("Enter value to convert");
            var source = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select source unit");
            var target = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select target unit");

            var quantity = new GenericQuantity<LengthUnit>(value, source);
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
        ConsoleHelper.WriteSubHeader("ADD TWO LENGTHS");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length1 = new GenericQuantity<LengthUnit>(value1, unit1);

            Console.WriteLine("\nSecond Length:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length2 = new GenericQuantity<LengthUnit>(value2, unit2);

            var result = _service.AddMeasurements(length1, length2);

            ConsoleHelper.WriteSuccess($"\n{length1} + {length2} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void AddMeasurementsWithTarget()
    {
        ConsoleHelper.WriteSubHeader("ADD TWO LENGTHS (WITH TARGET UNIT)");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length1 = new GenericQuantity<LengthUnit>(value1, unit1);

            Console.WriteLine("\nSecond Length:");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length2 = new GenericQuantity<LengthUnit>(value2, unit2);

            Console.WriteLine("\nTarget Unit:");
            var target = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select target unit for result");

            var result = _service.AddMeasurementsWithTarget(length1, length2, target);

            ConsoleHelper.WriteSuccess($"\n{length1} + {length2} = {result} (in {target.GetUnitSymbol()})");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void SubtractMeasurements()
    {
        ConsoleHelper.WriteSubHeader("SUBTRACT TWO LENGTHS");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length1 = new GenericQuantity<LengthUnit>(value1, unit1);

            Console.WriteLine("\nSecond Length (to subtract):");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length2 = new GenericQuantity<LengthUnit>(value2, unit2);

            var result = _service.SubtractMeasurements(length1, length2);

            ConsoleHelper.WriteSuccess($"\n{length1} - {length2} = {result}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void SubtractMeasurementsWithTarget()
    {
        ConsoleHelper.WriteSubHeader("SUBTRACT TWO LENGTHS (WITH TARGET UNIT)");

        try
        {
            Console.WriteLine("\nFirst Length:");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length1 = new GenericQuantity<LengthUnit>(value1, unit1);

            Console.WriteLine("\nSecond Length (to subtract):");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length2 = new GenericQuantity<LengthUnit>(value2, unit2);

            Console.WriteLine("\nTarget Unit:");
            var target = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select target unit for result");

            var result = _service.SubtractMeasurementsWithTarget(length1, length2, target);

            ConsoleHelper.WriteSuccess($"\n{length1} - {length2} = {result} (in {target.GetUnitSymbol()})");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void DivideMeasurements()
    {
        ConsoleHelper.WriteSubHeader("DIVIDE TWO LENGTHS (GET RATIO)");

        try
        {
            Console.WriteLine("\nFirst Length (dividend):");
            double value1 = ConsoleHelper.ReadDouble("Enter value");
            var unit1 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length1 = new GenericQuantity<LengthUnit>(value1, unit1);

            Console.WriteLine("\nSecond Length (divisor):");
            double value2 = ConsoleHelper.ReadDouble("Enter value");
            var unit2 = (LengthUnit)GenericUnitSelector.SelectLengthUnit("Select unit");
            var length2 = new GenericQuantity<LengthUnit>(value2, unit2);

            double ratio = _service.DivideMeasurements(length1, length2);

            ConsoleHelper.WriteSuccess($"\n{length1} ÷ {length2} = {ratio:F4}");
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Error: {ex.Message}");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    public void ViewMeasurements()
    {
        var measurements = _service.GetMeasurementsByType<LengthUnit>();
        if (!measurements.Any())
        {
            ConsoleHelper.WriteInfo("  No length measurements found.");
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