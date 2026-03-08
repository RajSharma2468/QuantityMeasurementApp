namespace QuantityMeasurementApp.Core.ValueObjects
{
    public class Inch
    {
        public double Value { get; }

        public Inch(double value)
        {
            Value = value;
        }

        public double ToFoot()
        {
            return Value / 12;
        }

        public override string ToString()
        {
            return Value + " INCH";
        }
    }
}