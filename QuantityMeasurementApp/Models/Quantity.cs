using System;

namespace QuantityMeasurementApp.Models
{
    public class Quantity
    {
        private readonly double value;
        private readonly LengthUnit unit;

        public Quantity(double value, LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        private double ToBase()
        {
            return unit.ToBaseUnit(value);
        }

        public bool Compare(Quantity other)
        {
            if (other == null)
                return false;

            double difference = Math.Abs(this.ToBase() - other.ToBase());
            return difference < 0.0001;
        }
    }
}