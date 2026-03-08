using QuantityMeasurementApp.UI.Helpers;
using QuantityMeasurementApp.UI.Menus;

namespace QuantityMeasurementApp.UI.Menus;

public class MainMenu
{
    private readonly GenericLengthMenu _lengthMenu;
    private readonly GenericWeightMenu _weightMenu;

    public MainMenu()
    {
        _lengthMenu = new GenericLengthMenu();
        _weightMenu = new GenericWeightMenu();
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteHeader("QUANTITY MEASUREMENT APPLICATION");
            Console.WriteLine("\n1. Length Measurements");
            Console.WriteLine("2. Weight Measurements");
            Console.WriteLine("3. View All Measurements");
            Console.WriteLine("4. Clear All Measurements");
            Console.WriteLine("5. Exit");

            int choice = ConsoleHelper.ReadInt("\nSelect option", 1, 5);

            switch (choice)
            {
                case 1:
                    _lengthMenu.Run();
                    break;
                case 2:
                    _weightMenu.Run();
                    break;
                case 3:
                    ViewAllMeasurements();
                    break;
                case 4:
                    ClearAllMeasurements();
                    break;
                case 5:
                    ConsoleHelper.WriteInfo("\nThank you for using the application!");
                    return;
            }
        }
    }

    private void ViewAllMeasurements()
    {
        ConsoleHelper.WriteSubHeader("ALL MEASUREMENTS");
        
        Console.WriteLine("\nLENGTH MEASUREMENTS:");
        _lengthMenu.ViewMeasurements();
        
        Console.WriteLine("\nWEIGHT MEASUREMENTS:");
        _weightMenu.ViewMeasurements();

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ClearAllMeasurements()
    {
        if (ConsoleHelper.ReadConfirmation("Are you sure you want to clear all measurements?"))
        {
            _lengthMenu.ClearMeasurements();
            _weightMenu.ClearMeasurements();
            ConsoleHelper.WriteSuccess("All measurements cleared successfully.");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }
}