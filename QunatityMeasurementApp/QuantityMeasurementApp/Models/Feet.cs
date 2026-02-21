using System;

namespace QuantityMeasurementApp.Models
{
    // This class represents a measurement in Feet
    public class Feet : IEquatable<Feet>
    {
        // Property to store the value in feet
        public double Value { get; set; }

        // Constructor to initialize the feet value
        public Feet(double value)
        {
            Value = value;
        }

        // Override Equals method with correct nullability (Fix for CS8765)
        public override bool Equals(object? obj)
        {
            // Check if object is null or not of type Feet
            if (obj is not Feet other)
            {
                return false;
            }

            // Compare the values of both objects
            return Value.Equals(other.Value);
        }

        // Strongly typed Equals method
        public bool Equals(Feet? other)
        {
            // Check if other object is null
            if (other is null)
            {
                return false;
            }

            // Compare values
            return Value.Equals(other.Value);
        }

        // Override GetHashCode when Equals is overridden
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        // Method to convert Feet to Inches
        public double ToInches()
        {
            return Value * 12;
        }
    }
}