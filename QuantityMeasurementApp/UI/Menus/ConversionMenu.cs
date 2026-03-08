using QuantityMeasurementApp.Services;
using QuantityMeasurementApp.UI.Helpers;
using QuantityMeasurementApp.Utils.Validators;

namespace QuantityMeasurementApp.UI.Menus
{
    public class ConversionMenu
    {
        public void Run()
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            ConsoleHelper.Print("Enter Value");
            double value = InputValidator.ReadDouble();

            var from = UnitSelector.SelectUnit();
            var to = UnitSelector.SelectUnit();

            var result = service.Convert(value, from, to);

            ConsoleHelper.Print("Result " + result.Value + " " + result.Unit);
        }
    }
}