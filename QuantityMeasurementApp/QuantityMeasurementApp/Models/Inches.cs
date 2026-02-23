using System;

namespace QuantityMeasurementApp.Models
{
    // This class represents Inches measurement (UC2 Extension)
    public class Inches : IEquatable<Inches>
    {
        // Property to store inches value (Encapsulation)
        public double Value { get; private set; }

        // Constructor to initialize inches value
        public Inches(double value)
        {
            Value = value;
        }

        // Override Equals method for object equality
        public override bool Equals(object? obj)
        {
            // Return false if object is null or not Inches type
            if (obj is not Inches other)
            {
                return false;
            }

            // Compare numeric values (Value-Based Equality)
            return Value.Equals(other.Value);
        }

        // Strongly typed Equals method
        public bool Equals(Inches? other)
        {
            // Null safety check
            if (other is null)
            {
                return false;
            }

            // Compare inches values
            return Value.Equals(other.Value);
        }

        // Override GetHashCode (Required with Equals override)
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}