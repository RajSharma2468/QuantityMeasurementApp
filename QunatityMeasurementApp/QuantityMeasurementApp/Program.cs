using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    // Entry point class of the application
    class Program
    {
        // Main method - execution starts from here
        static void Main(string[] args)
        {
            // Creating object of service class (Business Logic Layer)
            QuantityMeasurementService service = new QuantityMeasurementService();

            // Boolean flag to control the menu loop
            bool exit = false;

            // Loop will run until user chooses to exit
            while (!exit)
            {
                // Displaying menu options to the user
                Console.WriteLine("\n===== Quantity Measurement Menu =====");
                Console.WriteLine("1. Compare Two Feet Values (UC1)");
                Console.WriteLine("2. Add Two Feet Values");
                Console.WriteLine("3. Convert Feet to Inches");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                // ReadLine() returns string? (nullable)
                // Using ?? "" to avoid CS8600 null warning
                string choice = Console.ReadLine() ?? "";

                // Switch case to handle menu selection
                switch (choice)
                {
                    case "1":
                        // UC1: Compare two feet values for equality
                        Console.WriteLine("\n--- Compare Two Feet Values ---");

                        // Taking first feet input from user
                        Console.Write("Enter first feet value: ");
                        string input1 = Console.ReadLine() ?? "0"; // null safety
                        double feetValue1 = Convert.ToDouble(input1); // convert string to double

                        // Taking second feet input from user
                        Console.Write("Enter second feet value: ");
                        string input2 = Console.ReadLine() ?? "0"; // null safety
                        double feetValue2 = Convert.ToDouble(input2);

                        // Creating Feet objects (Model Layer)
                        Feet feet1 = new Feet(feetValue1);
                        Feet feet2 = new Feet(feetValue2);

                        // Calling service method to compare values (Service Layer)
                        bool result = service.CompareFeet(feet1, feet2);

                        // Displaying comparison result
                        Console.WriteLine("Equality Result: " + result);
                        break;

                    case "2":
                        // Feature: Add two feet values
                        Console.WriteLine("\n--- Add Two Feet Values ---");

                        // Input for first feet value
                        Console.Write("Enter first feet value: ");
                        string addInput1 = Console.ReadLine() ?? "0";
                        double addFeet1 = Convert.ToDouble(addInput1);

                        // Input for second feet value
                        Console.Write("Enter second feet value: ");
                        string addInput2 = Console.ReadLine() ?? "0";
                        double addFeet2 = Convert.ToDouble(addInput2);

                        // Creating Feet objects
                        Feet f1 = new Feet(addFeet1);
                        Feet f2 = new Feet(addFeet2);

                        // Calling service method to add feet values
                        double sum = service.AddFeet(f1, f2);

                        // Displaying sum result
                        Console.WriteLine("Total Feet = " + sum);
                        break;

                    case "3":
                        // Feature: Convert Feet to Inches
                        Console.WriteLine("\n--- Convert Feet to Inches ---");

                        // Taking feet input from user
                        Console.Write("Enter feet value: ");
                        string feetInput = Console.ReadLine() ?? "0";
                        double feetValue = Convert.ToDouble(feetInput);

                        // Creating Feet object
                        Feet feet = new Feet(feetValue);

                        // Calling service method for conversion
                        double inches = service.ConvertFeetToInches(feet);

                        // Displaying converted value
                        Console.WriteLine("Inches = " + inches);
                        break;

                    case "4":
                        // Exit option selected
                        exit = true;
                        Console.WriteLine("Exiting the application. Thank you!");
                        break;

                    default:
                        // Handles invalid menu input
                        Console.WriteLine("Invalid choice! Please select a valid option (1-4).");
                        break;
                }
            }
        }
    }
}