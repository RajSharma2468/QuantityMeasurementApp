using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class FeetTests
    {
        // Test equality for same feet values
        [Test]
        public void GivenSameFeetValues_ShouldReturnTrue()
        {
            Feet f1 = new Feet(5);
            Feet f2 = new Feet(5);

            bool result = f1.Equals(f2);

            Assert.That(result, Is.True, "Feet values 5 and 5 should be equal");
        }

        // Test inequality for different feet values
        [Test]
        public void GivenDifferentFeetValues_ShouldReturnFalse()
        {
            Feet f1 = new Feet(5);
            Feet f2 = new Feet(6);

            bool result = f1.Equals(f2);

            Assert.That(result, Is.False, "Feet values 5 and 6 should NOT be equal");
        }

        // Test null comparison
        [Test]
        public void GivenFeetAndNull_ShouldReturnFalse()
        {
            Feet f1 = new Feet(5);

            bool result = f1.Equals(null);

            Assert.That(result, Is.False, "Feet should not be equal to null");
        }

        // Test comparison with same reference (reflexive property)
        [Test]
        public void GivenSameReference_ShouldReturnTrue()
        {
            Feet f1 = new Feet(5);

            bool result = f1.Equals(f1);

            Assert.That(result, Is.True, "Feet should be equal to itself (reflexive)");
        }

        // Test comparison with non-Feet object
        [Test]
        public void GivenNonFeetObject_ShouldReturnFalse()
        {
            Feet f1 = new Feet(5);
            object nonFeet = "Not a Feet Object";

            bool result = f1.Equals(nonFeet);

            Assert.That(result, Is.False, "Feet should not be equal to a non-Feet object");
        }
    }
}