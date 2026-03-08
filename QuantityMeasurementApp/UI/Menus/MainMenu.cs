using QuantityMeasurementApp.UI.Helpers;
using QuantityMeasurementApp.Utils.Validators;

namespace QuantityMeasurementApp.UI.Menus
{
    public class MainMenu
    {
        public int Show()
        {
            ConsoleHelper.Print("1 Conversion");
            ConsoleHelper.Print("2 Addition");
            ConsoleHelper.Print("3 Comparison");
            ConsoleHelper.Print("4 Exit");

            return InputValidator.ReadInt();
        }
    }
}