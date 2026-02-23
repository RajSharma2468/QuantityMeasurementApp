using System;

namespace QuantityMeasurementApp.Models
{
    // This class represents Feet measurement (UC1)
    public class Feet : IEquatable<Feet>
    {
        // Property to store feet value (Encapsulation)
        public double Value { get; private set; }

        // Constructor to initialize feet value
        public Feet(double value)
        {
            Value = value;
        }

        // Override Equals method from Object class (Value-Based Equality)
        public override bool Equals(object? obj)
        {
            // Null check and type check (Type Safety)
            if (obj is not Feet other)
            {
                return false;
            }

            // Compare double values safely
            return Value.Equals(other.Value);
        }

        // Strongly typed Equals method (Best Practice)
        public bool Equals(Feet? other)
        {
            // Null safety check
            if (other is null)
            {
                return false;
            }

            // Compare the actual measurement values
            return Value.Equals(other.Value);
        }

        // Override GetHashCode when Equals is overridden
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}