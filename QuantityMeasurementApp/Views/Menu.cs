using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Views
{
    public class Menu
    {
        private QuantityMeasurementService service = new QuantityMeasurementService();

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("1. Convert Length");
                Console.WriteLine("2. Compare Length");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");

                string? choice = Console.ReadLine();
                if (choice == null)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                if (choice == "1")
                    ConvertMenu();
                else if (choice == "2")
                    CompareMenu();
                else if (choice == "3")
                    break;
                else
                    Console.WriteLine("Invalid choice");
            }
        }

        private void ConvertMenu()
        {
            Console.Write("Enter value: ");
            string? valueInput = Console.ReadLine();
            if (valueInput == null) return;

            double value = Convert.ToDouble(valueInput);

            Console.WriteLine("Select From Unit: 0-FEET 1-INCH 2-YARD 3-CENTIMETER");
            LengthUnit from = (LengthUnit)Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Select To Unit: 0-FEET 1-INCH 2-YARD 3-CENTIMETER");
            LengthUnit to = (LengthUnit)Convert.ToInt32(Console.ReadLine());

            Quantity result = service.Convert(value, from, to);

            Console.WriteLine("Converted Value: " + result.Value);
        }

        private void CompareMenu()
        {
            Console.Write("Enter first value: ");
            double v1 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter first unit (0-3): ");
            LengthUnit u1 = (LengthUnit)Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second value: ");
            double v2 = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter second unit (0-3): ");
            LengthUnit u2 = (LengthUnit)Convert.ToInt32(Console.ReadLine());

            bool equal = service.AreEqual(v1, u1, v2, u2);

            Console.WriteLine("Are Equal: " + equal);
        }
    }
}