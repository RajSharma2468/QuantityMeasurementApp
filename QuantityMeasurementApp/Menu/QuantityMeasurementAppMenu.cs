using BusinessLayer.Interfaces;
using ModelLayer.Exceptions;
using ModelLayer.Models;
using System;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Menu
{
    public class QuantityMeasurementAppMenu
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementAppMenu(IQuantityMeasurementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void Run()
        {
            while (true)
            {
                DisplayMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PerformComparison();
                        break;
                    case "2":
                        PerformConversion();
                        break;
                    case "3":
                        PerformAddition();
                        break;
                    case "4":
                        PerformSubtraction();
                        break;
                    case "5":
                        PerformDivision();
                        break;
                    case "6":
                        ShowHistory();
                        break;
                    case "7":
                        Console.WriteLine("\nThank you for using Quantity Measurement App!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("======================================");
            Console.WriteLine("   QUANTITY MEASUREMENT APP MENU");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Compare Quantities");
            Console.WriteLine("2. Convert Quantity");
            Console.WriteLine("3. Add Quantities");
            Console.WriteLine("4. Subtract Quantities");
            Console.WriteLine("5. Divide Quantities");
            Console.WriteLine("6. View Operation History");
            Console.WriteLine("7. Exit");
            Console.WriteLine("======================================");
            Console.Write("Enter your choice: ");
        }

        private void PerformComparison()
        {
            Console.Clear();
            Console.WriteLine("=== COMPARE QUANTITIES ===\n");

            try
            {
                Console.WriteLine("Enter first quantity:");
                var q1 = GetQuantityInput();

                Console.WriteLine("\nEnter second quantity:");
                var q2 = GetQuantityInput();

                var result = _service.Compare(q1, q2);

                Console.WriteLine($"\nResult: Quantities are {(result.Value == 1 ? "EQUAL" : "NOT EQUAL")}");
                DisplaySuccessMessage();
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayErrorMessage(ex.Message);
            }
        }

        private void PerformConversion()
        {
            Console.Clear();
            Console.WriteLine("=== CONVERT QUANTITY ===\n");

            try
            {
                Console.WriteLine("Enter source quantity:");
                var source = GetQuantityInput();

                Console.Write("\nEnter target unit: ");
                string targetUnit = Console.ReadLine();

                // Service layer will throw exception if target unit is invalid
                var result = _service.Convert(source, targetUnit);

                Console.WriteLine($"\nResult: {source.Value} {source.UnitName} = {result.Value} {result.UnitName}");
                DisplaySuccessMessage();
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayErrorMessage($"Conversion failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Unexpected error: {ex.Message}");
            }
        }

        private void PerformAddition()
        {
            Console.Clear();
            Console.WriteLine("=== ADD QUANTITIES ===\n");

            try
            {
                Console.WriteLine("Enter first quantity:");
                var q1 = GetQuantityInput();

                Console.WriteLine("\nEnter second quantity:");
                var q2 = GetQuantityInput();

                var result = _service.Add(q1, q2);

                Console.WriteLine($"\nResult: {q1.Value} {q1.UnitName} + {q2.Value} {q2.UnitName} = {result.Value} {result.UnitName}");
                DisplaySuccessMessage();
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayErrorMessage($"Addition failed: {ex.Message}");
            }
        }

        private void PerformSubtraction()
        {
            Console.Clear();
            Console.WriteLine("=== SUBTRACT QUANTITIES ===\n");

            try
            {
                Console.WriteLine("Enter first quantity:");
                var q1 = GetQuantityInput();

                Console.WriteLine("\nEnter second quantity:");
                var q2 = GetQuantityInput();

                var result = _service.Subtract(q1, q2);

                Console.WriteLine($"\nResult: {q1.Value} {q1.UnitName} - {q2.Value} {q2.UnitName} = {result.Value} {result.UnitName}");
                DisplaySuccessMessage();
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayErrorMessage($"Subtraction failed: {ex.Message}");
            }
        }

        private void PerformDivision()
        {
            Console.Clear();
            Console.WriteLine("=== DIVIDE QUANTITIES ===\n");

            try
            {
                Console.WriteLine("Enter first quantity (dividend):");
                var q1 = GetQuantityInput();

                Console.WriteLine("\nEnter second quantity (divisor):");
                var q2 = GetQuantityInput();

                var result = _service.Divide(q1, q2);

                Console.WriteLine($"\nResult: {q1.Value} {q1.UnitName} / {q2.Value} {q2.UnitName} = {result.Value:F4}");
                DisplaySuccessMessage();
            }
            catch (QuantityMeasurementException ex)
            {
                DisplayErrorMessage($"Division failed: {ex.Message}");
            }
            catch (DivideByZeroException)
            {
                DisplayErrorMessage("Cannot divide by zero!");
            }
        }

        private void ShowHistory()
        {
            Console.Clear();
            Console.WriteLine("=== OPERATION HISTORY ===\n");

            var history = _service.GetOperationHistory();

            if (history.Count == 0)
            {
                Console.WriteLine("No operations performed yet.");
            }
            else
            {
                for (int i = 0; i < history.Count; i++)
                {
                    Console.WriteLine($"--- Operation #{i + 1} ---");
                    Console.WriteLine(history[i].ToString());
                    Console.WriteLine("--------------------------------------");
                }
            }
        }

        private QuantityDTO GetQuantityInput()
        {
            Console.Write("Value: ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                throw new QuantityMeasurementException("Invalid value format. Please enter a number.");
            }

            Console.Write("Unit (e.g., FEET, INCH, CELSIUS, GRAM): ");
            string unit = Console.ReadLine();

            Console.Write("Measurement Type (Length/Weight/Volume/Temperature): ");
            string measurementType = Console.ReadLine();

            return new QuantityDTO(value, unit, measurementType);
        }

        private void DisplaySuccessMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✓ Operation completed successfully!");
            Console.ResetColor();
        }

        private void DisplayErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n✗ Error: {message}");
            Console.ResetColor();
        }
    }
}