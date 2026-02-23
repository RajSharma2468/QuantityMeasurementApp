using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    // Service layer handles business logic for UC1 and UC2
    public class QuantityMeasurementService
    {
        // Static method to compare two Feet values (UC1)
        public static bool CompareFeet(double value1, double value2)
        {
            // Create Feet objects
            Feet feet1 = new Feet(value1);
            Feet feet2 = new Feet(value2);

            // Call equality method
            return feet1.Equals(feet2);
        }

        // Static method to compare two Inches values (UC2)
        public static bool CompareInches(double value1, double value2)
        {
            // Create Inches objects
            Inches inch1 = new Inches(value1);
            Inches inch2 = new Inches(value2);

            // Call equality method
            return inch1.Equals(inch2);
        }
    }
}