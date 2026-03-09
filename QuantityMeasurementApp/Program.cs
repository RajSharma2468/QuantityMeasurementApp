using QuantityMeasurementApp.UI.Menus;

namespace QuantityMeasurementApp;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "Quantity Measurement Application";
        
        try
        {
            var mainMenu = new MainMenu();
            mainMenu.Run();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}