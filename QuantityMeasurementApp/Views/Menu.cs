using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Views
{
    public class Menu
    {
        public void Start()
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            while (true)
            {
                Console.WriteLine("------ Quantity Measurement App ------");
                Console.WriteLine("1. Add Two Lengths");
                Console.WriteLine("2. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    try
                    {
                        Quantity q1 = ReadQuantity("First");
                        Quantity q2 = ReadQuantity("Second");

                        Quantity result = service.Add(q1, q2);

                        Console.WriteLine("Result: " + result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                else if (choice == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }
        }

        private Quantity ReadQuantity(string label)
        {
            Console.Write("Enter " + label + " value: ");
            string valueInput = Console.ReadLine() ?? "0";
            double value = Convert.ToDouble(valueInput);

            Console.WriteLine("Select Unit:");
            Console.WriteLine("1. FEET");
            Console.WriteLine("2. INCHES");
            Console.WriteLine("3. YARDS");
            Console.WriteLine("4. CENTIMETERS");
            Console.Write("Enter choice: ");

            string unitInput = Console.ReadLine() ?? "1";

            LengthUnit unit;

            switch (unitInput)
            {
                case "1":
                    unit = LengthUnit.FEET;
                    break;
                case "2":
                    unit = LengthUnit.INCHES;
                    break;
                case "3":
                    unit = LengthUnit.YARDS;
                    break;
                case "4":
                    unit = LengthUnit.METER;
                    break;
                default:
                    throw new ArgumentException("Invalid unit");
            }

            return new Quantity(value, unit);
        }
    }
}