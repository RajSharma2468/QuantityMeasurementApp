using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Utils.Validators;

namespace QuantityMeasurementApp.UI.Helpers
{
    public static class UnitSelector
    {
        public static LengthUnit SelectUnit()
        {
            ConsoleHelper.Print("Select Unit");
            ConsoleHelper.Print("0 FEET");
            ConsoleHelper.Print("1 INCHES");
            ConsoleHelper.Print("2 YARDS");
            ConsoleHelper.Print("3 CENTIMETERS");

            int input = InputValidator.ReadInt();

            return (LengthUnit)input;
        }
    }
}