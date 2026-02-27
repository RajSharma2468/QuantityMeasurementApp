namespace QuantityMeasurementApp.Models
{
    public class Feet : Quantity
    {
        public Feet(double value) : base(value, LengthUnit.FEET)
        {
        }
    }
}