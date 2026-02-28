using System;
using QuantityMeasurementApp.Views;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var menu = new Menu();
                menu.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
            }

            Console.WriteLine("Program terminated. Press any key to exit...");
            Console.ReadKey();
        }
    }
}