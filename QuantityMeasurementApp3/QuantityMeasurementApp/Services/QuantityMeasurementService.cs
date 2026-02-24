using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    // Service layer handling all UC1, UC2 and UC3 comparisons
    public class QuantityMeasurementService
    {
        // ================= UC1 =================
        // Compare two feet values
        public static bool CompareFeet(double value1, double value2)
        {
            return value1 == value2;
        }

        // ================= UC2 =================
        // Compare two inches values
        public static bool CompareInches(double value1, double value2)
        {
            return value1 == value2;
        }

        // ================= UC3 =================
        // Generic method (DRY Principle)
        // This is the method your code is missing
        public static bool AreEqual(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
        {
            QuantityLength q1 = new QuantityLength(value1, unit1);
            QuantityLength q2 = new QuantityLength(value2, unit2);

            return q1.Equals(q2);
        }

        // (Optional) Keep old name also if tests use CompareLength
        public static bool CompareLength(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
        {
            return AreEqual(value1, unit1, value2, unit2);
        }
    }
}