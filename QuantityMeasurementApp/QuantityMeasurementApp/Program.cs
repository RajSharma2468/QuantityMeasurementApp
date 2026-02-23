using System;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    // Main Presentation Layer (Menu Driven Program)
    class Program
    {
        static void Main(string[] args)
        {
            // Boolean flag to control menu loop
            bool exit = false;

            // Loop will run until user chooses Exit
            while (!exit)
            {
                // Display menu options
                Console.WriteLine("\n===== Quantity Measurement App (UC1 + UC2) =====");
                Console.WriteLine("1. Compare Feet Equality (UC1)");
                Console.WriteLine("2. Compare Inches Equality (UC2)");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                // Read user input safely (Null Warning Fix CS8600)
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        // UC1: Feet Equality
                        Console.Write("Enter first feet value: ");
                        double feet1 = Convert.ToDouble(Console.ReadLine() ?? "0");

                        Console.Write("Enter second feet value: ");
                        double feet2 = Convert.ToDouble(Console.ReadLine() ?? "0");

                        // Call service method
                        bool feetResult = QuantityMeasurementService.CompareFeet(feet1, feet2);

                        // Display result
                        Console.WriteLine("Feet Equality Result: " + feetResult);
                        break;

                    case "2":
                        // UC2: Inches Equality
                        Console.Write("Enter first inch value: ");
                        double inch1 = Convert.ToDouble(Console.ReadLine() ?? "0");

                        Console.Write("Enter second inch value: ");
                        double inch2 = Convert.ToDouble(Console.ReadLine() ?? "0");

                        // Call service method
                        bool inchResult = QuantityMeasurementService.CompareInches(inch1, inch2);

                        // Display result
                        Console.WriteLine("Inches Equality Result: " + inchResult);
                        break;

                    case "3":
                        // Exit the application
                        exit = true;
                        Console.WriteLine("Exiting Application...");
                        break;

                    default:
                        // Handle invalid input
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}