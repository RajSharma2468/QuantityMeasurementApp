using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Views
{
    public class Menu
    {
        private readonly QuantityMeasurementService service;

        public Menu()
        {
            service = new QuantityMeasurementService();
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("===== Quantity Measurement Menu =====");
                Console.WriteLine("1. Add two lengths");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string? choice = Console.ReadLine();
                if (choice == null) continue;

                if (choice == "0")
                    break;

                switch (choice)
                {
                    case "1":
                        AddLengths();
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }

                Console.WriteLine();
            }
        }

        private void AddLengths()
        {
            try
            {
                Console.Write("Enter first value: ");
                double value1 = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter first unit (FEET, INCHES, YARDS, CENTIMETERS): ");
                string? unit1Str = Console.ReadLine();
                LengthUnit unit1 = ParseUnit(unit1Str);

                Console.Write("Enter second value: ");
                double value2 = double.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter second unit (FEET, INCHES, YARDS, CENTIMETERS): ");
                string? unit2Str = Console.ReadLine();
                LengthUnit unit2 = ParseUnit(unit2Str);

                Console.Write("Enter target unit (FEET, INCHES, YARDS, CENTIMETERS): ");
                string? targetStr = Console.ReadLine();
                LengthUnit targetUnit = ParseUnit(targetStr);

                var q1 = new QuantityLength(value1, unit1);
                var q2 = new QuantityLength(value2, unit2);

                var result = service.Add(q1, q2, targetUnit);

                Console.WriteLine($"Result: {result.Value} {result.Unit}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private LengthUnit ParseUnit(string? input)
        {
            if (input == null) throw new ArgumentException("Unit cannot be null");

            if (!Enum.TryParse<LengthUnit>(input.ToUpper(), out var unit))
                throw new ArgumentException($"Invalid unit: {input}");

            return unit;
        }
    }
}