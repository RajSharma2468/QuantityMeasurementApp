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

        public void Start()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== Quantity Measurement App =====");
                Console.WriteLine("1. Compare Two Lengths");
                Console.WriteLine("2. Exit");
                Console.WriteLine("Enter choice:");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                int choice;
                if (!int.TryParse(input, out choice))
                    continue;

                if (choice == 1)
                {
                    Console.WriteLine("First Quantity:");
                    Quantity q1 = service.CreateQuantity();
                    if (q1 == null) continue;

                    Console.WriteLine("Second Quantity:");
                    Quantity q2 = service.CreateQuantity();
                    if (q2 == null) continue;

                    bool result = q1.Compare(q2);

                    if (result)
                        Console.WriteLine("Equal (true)");
                    else
                        Console.WriteLine("Not Equal (false)");
                }
                else if (choice == 2)
                {
                    break;
                }
            }
        }
    }
}