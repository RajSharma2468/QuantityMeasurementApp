using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Quantity Measurement App (UC1 + UC2 + UC3) ===");

            while (true)
            {
                Console.WriteLine("\nSelect Option:");
                Console.WriteLine("1. UC1 - Compare Feet");
                Console.WriteLine("2. UC2 - Compare Inches");
                Console.WriteLine("3. UC3 - Compare Generic Length (Feet & Inches)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    // UC1: Compare Feet
                    Console.Write("Enter first value in Feet: ");
                    double value1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter second value in Feet: ");
                    double value2 = Convert.ToDouble(Console.ReadLine());

                    bool result = QuantityMeasurementService.AreEqual(value1, LengthUnit.Feet, value2, LengthUnit.Feet);
                    Console.WriteLine(result ? "Equal" : "Not Equal");
                }
                else if (choice == "2")
                {
                    // UC2: Compare Inches
                    Console.Write("Enter first value in Inches: ");
                    double value1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter second value in Inches: ");
                    double value2 = Convert.ToDouble(Console.ReadLine());

                    bool result = QuantityMeasurementService.AreEqual(value1, LengthUnit.Inches, value2, LengthUnit.Inches);
                    Console.WriteLine(result ? "Equal" : "Not Equal");
                }
                else if (choice == "3")
                {
                    // UC3: Generic Comparison (Feet & Inches)
                    Console.Write("Enter first value: ");
                    double value1 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter first unit (Feet/Inches): ");
                    LengthUnit unit1 = Enum.Parse<LengthUnit>(Console.ReadLine()!, true);

                    Console.Write("Enter second value: ");
                    double value2 = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Enter second unit (Feet/Inches): ");
                    LengthUnit unit2 = Enum.Parse<LengthUnit>(Console.ReadLine()!, true);

                    bool result = QuantityMeasurementService.AreEqual(value1, unit1, value2, unit2);
                    Console.WriteLine(result ? "Equal" : "Not Equal");
                }
                else if (choice == "4")
                {
                    Console.WriteLine("Exiting Application...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
            }
        }
    }
}