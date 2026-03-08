using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Core.Domain.ValueObjects
{
    public class Feet
    {
        public double Value { get; }

        public Feet(double value)
        {
            Value = value;
        }

        public LengthUnit Unit()
        {
            return LengthUnit.FEET;
        }
    }
}