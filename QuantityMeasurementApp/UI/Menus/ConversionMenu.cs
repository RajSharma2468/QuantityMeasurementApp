using System;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Helpers;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.UI.Menus;

public class ConversionMenu
{
    private readonly QuantityMeasurementService _service;

    public ConversionMenu(QuantityMeasurementService service)
    {
        _service = service;
    }

    public void ShowWeightConversions()
    {
        ConsoleHelper.WriteSubHeader("WEIGHT CONVERSION TABLE");
        
        var testValues = new[] { 1.0, 2.5, 5.0 };
        
        foreach (var value in testValues)
        {
            Console.WriteLine($"\n{value} kg:");
            var kg = new QuantityWeight(value, WeightUnit.KILOGRAM);
            Console.WriteLine($"  → {kg.ConvertTo(WeightUnit.GRAM)}");
            Console.WriteLine($"  → {kg.ConvertTo(WeightUnit.POUND):F2} lb");
            
            Console.WriteLine($"\n{value} lb:");
            var lb = new QuantityWeight(value, WeightUnit.POUND);
            Console.WriteLine($"  → {lb.ConvertTo(WeightUnit.KILOGRAM):F2} kg");
            Console.WriteLine($"  → {lb.ConvertTo(WeightUnit.GRAM):F0} g");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }

    public void ShowLengthConversions()
    {
        ConsoleHelper.WriteSubHeader("LENGTH CONVERSION TABLE");
        
        var testValues = new[] { 1.0, 2.5, 5.0 };
        
        foreach (var value in testValues)
        {
            Console.WriteLine($"\n{value} ft:");
            var ft = new QuantityLength(value, LengthUnit.FEET);
            Console.WriteLine($"  → {ft.ConvertTo(LengthUnit.INCH)}");
            Console.WriteLine($"  → {ft.ConvertTo(LengthUnit.YARD)}");
            Console.WriteLine($"  → {ft.ConvertTo(LengthUnit.CENTIMETER):F1} cm");
        }

        ConsoleHelper.PressAnyKeyToContinue();
    }
}