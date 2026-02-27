namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit
    {
        FEET = 12,
        INCHES = 1,
        YARDS = 36,
        CENTIMETERS = 0
    }

    public static class LengthUnitExtension
    {
        public static double ToBaseUnit(this LengthUnit unit, double value)
        {
            if (unit == LengthUnit.CENTIMETERS)
                return value * 0.393701;

            return value * (double)unit;
        }
    }
}