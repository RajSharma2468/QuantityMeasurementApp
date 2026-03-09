using QuantityMeasurementApp.UI.Helpers;

namespace QuantityMeasurementApp.UI.Menus;

public class MainMenu
{
    private readonly GenericLengthMenu _lengthMenu;
    private readonly GenericWeightMenu _weightMenu;
    private readonly GenericVolumeMenu _volumeMenu;

    public MainMenu()
    {
        _lengthMenu = new GenericLengthMenu();
        _weightMenu = new GenericWeightMenu();
        _volumeMenu = new GenericVolumeMenu();
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            ConsoleHelper.WriteHeader("QUANTITY MEASUREMENT APPLICATION");
            Console.WriteLine("\n1. Length Measurements");
            Console.WriteLine("2. Weight Measurements");
            Console.WriteLine("3. Volume Measurements");
            Console.WriteLine("4. View All Measurements");
            Console.WriteLine("5. Clear All Measurements");
            Console.WriteLine("6. Exit");

            int choice = ConsoleHelper.ReadInt("\nSelect option", 1, 6);

            switch (choice)
            {
                case 1:
                    _lengthMenu.Run();
                    break;
                case 2:
                    _weightMenu.Run();
                    break;
                case 3:
                    _volumeMenu.Run();
                    break;
                case 4:
                    ViewAllMeasurements();
                    break;
                case 5:
                    ClearAllMeasurements();
                    break;
                case 6:
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
        
        Console.WriteLine("\nVOLUME MEASUREMENTS:");
        _volumeMenu.ViewMeasurements();

        ConsoleHelper.PressAnyKeyToContinue();
    }

    private void ClearAllMeasurements()
    {
        if (ConsoleHelper.ReadConfirmation("Are you sure you want to clear all measurements?"))
        {
            _lengthMenu.ClearMeasurements();
            _weightMenu.ClearMeasurements();
            _volumeMenu.ClearMeasurements();
            ConsoleHelper.WriteSuccess("All measurements cleared successfully.");
            ConsoleHelper.PressAnyKeyToContinue();
        }
    }
}