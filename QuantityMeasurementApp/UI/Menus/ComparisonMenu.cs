using QuantityMeasurementApp.Services;
using QuantityMeasurementApp.UI.Helpers;
using QuantityMeasurementApp.Utils.Validators;

namespace QuantityMeasurementApp.UI.Menus
{
    public class ComparisonMenu
    {
        public void Run()
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            ConsoleHelper.Print("Enter First Value");
            double v1 = InputValidator.ReadDouble();

            var u1 = UnitSelector.SelectUnit();

            ConsoleHelper.Print("Enter Second Value");
            double v2 = InputValidator.ReadDouble();

            var u2 = UnitSelector.SelectUnit();

            bool result = service.Compare(v1, u1, v2, u2);

            ConsoleHelper.Print("Equal " + result);
        }
    }
}