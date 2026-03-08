using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Core.Domain.ValueObjects
{
    public class Inch
    {
        public double Value { get; }

        public Inch(double value)
        {
            Value = value;
        }

        public LengthUnit Unit()
        {
            return LengthUnit.INCHES;
        }
    }
}