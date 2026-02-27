namespace QuantityMeasurementApp.Models
{
    public class Inch : Quantity
    {
        public Inch(double value) : base(value, LengthUnit.INCH)
        {
        }
    }
}