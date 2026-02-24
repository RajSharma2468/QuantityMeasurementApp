using System;

namespace QuantityMeasurementApp.Models
{
    public class Feet : IEquatable<Feet>
    {
        public double Value { get; private set; }

        public Feet(double value)
        {
            Value = value;
        }

        public bool Equals(Feet? other)
        {
            if (other is null)
                return false;

            return Math.Abs(Value - other.Value) < 0.0001;
        }

        public override bool Equals(object? obj)
        {
            return obj is Feet other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}