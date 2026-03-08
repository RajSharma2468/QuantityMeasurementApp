namespace QuantityMeasurementApp.Core.ValueObjects
{
    public class Feet
    {
        public double Value { get; }

        public Feet(double value)
        {
            Value = value;
        }

        public double ToInch()
        {
            return Value * 12;
        }

        public override string ToString()
        {
            return Value + " FOOT";
        }
    }
}