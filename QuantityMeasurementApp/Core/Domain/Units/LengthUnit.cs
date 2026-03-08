namespace QuantityMeasurementApp.Core.Domain.Units
{
    public enum LengthUnit
    {
        FEET,
        INCHES,
        YARDS,
        CENTIMETERS
    }

    public static class LengthUnitExtensions
    {
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            switch (unit)
            {
                case LengthUnit.FEET:
                    return value;

                case LengthUnit.INCHES:
                    return value / 12.0;

                case LengthUnit.YARDS:
                    return value * 3.0;

                case LengthUnit.CENTIMETERS:
                    return value / 30.48;

                default:
                    return value;
            }
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            switch (unit)
            {
                case LengthUnit.FEET:
                    return baseValue;

                case LengthUnit.INCHES:
                    return baseValue * 12.0;

                case LengthUnit.YARDS:
                    return baseValue / 3.0;

                case LengthUnit.CENTIMETERS:
                    return baseValue * 30.48;

                default:
                    return baseValue;
            }
        }
    }
}