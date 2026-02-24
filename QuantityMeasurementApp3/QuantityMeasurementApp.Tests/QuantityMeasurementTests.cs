using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    // Test Fixture class for UC1, UC2 and UC3
    [TestFixture]
    public class QuantityMeasurementTests
    {
        // =========================
        // UC1 - Feet Equality Tests
        // =========================

        // Test when two feet values are same
        [Test]
        public void CompareFeet_SameValue_ReturnsTrue()
        {
            bool result = QuantityMeasurementService.CompareFeet(1.0, 1.0);
            Assert.That(result, Is.True);
        }

        // Test when two feet values are different
        [Test]
        public void CompareFeet_DifferentValue_ReturnsFalse()
        {
            bool result = QuantityMeasurementService.CompareFeet(1.0, 2.0);
            Assert.That(result, Is.False);
        }

        // =========================
        // UC2 - Inches Equality Tests
        // =========================

        // Test when two inch values are same
        [Test]
        public void CompareInches_SameValue_ReturnsTrue()
        {
            bool result = QuantityMeasurementService.CompareInches(12.0, 12.0);
            Assert.That(result, Is.True);
        }

        // Test when two inch values are different
        [Test]
        public void CompareInches_DifferentValue_ReturnsFalse()
        {
            bool result = QuantityMeasurementService.CompareInches(12.0, 24.0);
            Assert.That(result, Is.False);
        }

        // =========================
        // UC3 - Generic Quantity Tests
        // =========================

        // Test Feet to Feet equality
        [Test]
        public void CompareLength_FeetToFeet_SameValue_ReturnsTrue()
        {
            bool result = QuantityMeasurementService.CompareLength(
                1.0, LengthUnit.FEET,
                1.0, LengthUnit.FEET);

            Assert.That(result, Is.True);
        }

        // Test Inch to Inch equality
        [Test]
        public void CompareLength_InchToInch_SameValue_ReturnsTrue()
        {
            bool result = QuantityMeasurementService.CompareLength(
                12.0, LengthUnit.INCH,
                12.0, LengthUnit.INCH);

            Assert.That(result, Is.True);
        }

        // Test Feet to Inch equivalent (1 ft = 12 inch)
        [Test]
        public void CompareLength_FeetToInch_EquivalentValue_ReturnsTrue()
        {
            bool result = QuantityMeasurementService.CompareLength(
                1.0, LengthUnit.FEET,
                12.0, LengthUnit.INCH);

            Assert.That(result, Is.True);
        }

        // Test different length values
        [Test]
        public void CompareLength_DifferentValues_ReturnsFalse()
        {
            bool result = QuantityMeasurementService.CompareLength(
                1.0, LengthUnit.FEET,
                24.0, LengthUnit.INCH);

            Assert.That(result, Is.False);
        }

        // =========================
        // Equality Contract Tests (Important for UC3)
        // =========================

        // Reflexive property test (same reference)
        [Test]
        public void Quantity_Equals_SameReference_ReturnsTrue()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.That(quantity.Equals(quantity), Is.True);
        }

        // Null comparison test
        [Test]
        public void Quantity_Equals_Null_ReturnsFalse()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.FEET);
            Assert.That(quantity.Equals(null), Is.False);
        }

        // Different type comparison test
        [Test]
        public void Quantity_Equals_DifferentType_ReturnsFalse()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.FEET);
            object obj = "Invalid Type";
            Assert.That(quantity.Equals(obj), Is.False);
        }
    }
}