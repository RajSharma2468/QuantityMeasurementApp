using System;

namespace QuantityMeasurementApp.Models
{
    // Generic Quantity class for UC3 (DRY Principle)
    public class QuantityLength : IEquatable<QuantityLength>
    {
        // Value of measurement
        public double Value { get; }

        // Unit of measurement (Feet or Inches)
        public LengthUnit Unit { get; }

        // Constructor
        public QuantityLength(double value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        // Convert any unit to base unit (Feet)
        private double ToFeet()
        {
            if (Unit == LengthUnit.Feet)
                return Value;

            if (Unit == LengthUnit.Inches)
                return Value / 12.0;

            throw new InvalidOperationException("Unsupported Unit");
        }

        // Override Equals for value-based equality (UC3 core logic)
        public override bool Equals(object? obj)
        {
            if (obj is not QuantityLength other)
                return false;

            return Math.Abs(this.ToFeet() - other.ToFeet()) < 0.0001;
        }

        // Strongly typed Equals
        public bool Equals(QuantityLength? other)
        {
            if (other is null)
                return false;

            return Math.Abs(this.ToFeet() - other.ToFeet()) < 0.0001;
        }

        // Override GetHashCode (required with Equals)
        public override int GetHashCode()
        {
            return ToFeet().GetHashCode();
        }
    }
}