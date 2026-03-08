using QuantityMeasurementApp.UI.Menus;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();

            while (true)
            {
                int choice = menu.Show();

                if (choice == 1)
                {
                    new ConversionMenu().Run();
                }
                else if (choice == 2)
                {
                    new ArithmeticMenu().Run();
                }
                else if (choice == 3)
                {
                    new ComparisonMenu().Run();
                }
                else if (choice == 4)
                {
                    break;
                }
            }
        }
    }
}
