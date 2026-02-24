using System;

namespace QuantityMeasurementApp.Models
{
    public class Inches : IEquatable<Inches>
    {
        public double Value { get; private set; }

        public Inches(double value)
        {
            Value = value;
        }

        public bool Equals(Inches? other)
        {
            if (other is null)
                return false;

            return Math.Abs(Value - other.Value) < 0.0001;
        }

        public override bool Equals(object? obj)
        {
            return obj is Inches other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}