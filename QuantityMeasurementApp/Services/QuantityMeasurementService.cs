using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public Quantity Convert(double value, LengthUnit from, LengthUnit to)
        {
            Quantity quantity = new Quantity(value, from);
            return quantity.ConvertTo(to);
        }

        public bool AreEqual(double v1, LengthUnit u1, double v2, LengthUnit u2)
        {
            Quantity q1 = new Quantity(v1, u1);
            Quantity q2 = new Quantity(v2, u2);
            return q1.Equals(q2);
        }
    }
}