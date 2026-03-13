using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using QuantityMeasurementApp.Menu;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;
using System;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("   QUANTITY MEASUREMENT APP");
            Console.WriteLine("   N-Tier Architecture (UC15)");
            Console.WriteLine("======================================");
            Console.WriteLine();

            try
            {
                // Initialize dependencies using Factory pattern
                IQuantityRepository repository = QuantityRepository.Instance;
                IQuantityMeasurementService service = new QuantityMeasurementService(repository);

                // Create and run menu (Controller)
                var menu = new QuantityMeasurementAppMenu(service);
                menu.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fatal error: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
