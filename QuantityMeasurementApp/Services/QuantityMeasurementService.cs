using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public Quantity CreateQuantity()
        {
            Console.WriteLine("Enter value:");
            string? valueInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(valueInput))
                return null!;

            double value;
            if (!double.TryParse(valueInput, out value))
                return null!;

            Console.WriteLine("Select Unit:");
            Console.WriteLine("1. FEET");
            Console.WriteLine("2. INCHES");
            Console.WriteLine("3. YARDS");
            Console.WriteLine("4. CENTIMETERS");

            string? unitInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(unitInput))
                return null!;

            int choice;
            if (!int.TryParse(unitInput, out choice))
                return null!;

            switch (choice)
            {
                case 1:
                    return new Quantity(value, LengthUnit.FEET);
                case 2:
                    return new Quantity(value, LengthUnit.INCHES);
                case 3:
                    return new Quantity(value, LengthUnit.YARDS);
                case 4:
                    return new Quantity(value, LengthUnit.CENTIMETERS);
                default:
                    return null!;
            }
        }
    }
}