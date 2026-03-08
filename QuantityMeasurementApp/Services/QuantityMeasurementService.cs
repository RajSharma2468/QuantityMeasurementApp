using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public Quantity Convert(double value, LengthUnit from, LengthUnit to)
        {
            Quantity q = new Quantity(value, from);
            return q.ConvertTo(to);
        }

        public Quantity Add(double v1, LengthUnit u1, double v2, LengthUnit u2, LengthUnit target)
        {
            Quantity q1 = new Quantity(v1, u1);
            Quantity q2 = new Quantity(v2, u2);

            return q1.Add(q2, target);
        }

        public bool Compare(double v1, LengthUnit u1, double v2, LengthUnit u2)
        {
            Quantity q1 = new Quantity(v1, u1);
            Quantity q2 = new Quantity(v2, u2);

            return q1.Equals(q2);
        }
    }
}