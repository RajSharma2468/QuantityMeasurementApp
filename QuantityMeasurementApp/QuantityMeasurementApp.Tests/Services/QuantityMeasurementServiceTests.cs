using NUnit.Framework;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestFixture]
    public class QuantityMeasurementServiceTests
    {
        // UC1 Test: Feet same value
        [Test]
        public void CompareFeet_SameValue_ReturnsTrue()
        {
            bool result = QuantityMeasurementService.CompareFeet(1.0, 1.0);

            // Correct NUnit assertion
            Assert.That(result, Is.True);
        }

        // UC1 Test: Feet different value
        [Test]
        public void CompareFeet_DifferentValue_ReturnsFalse()
        {
            bool result = QuantityMeasurementService.CompareFeet(1.0, 2.0);

            // Correct NUnit assertion
            Assert.That(result, Is.False);
        }

        // UC2 Test: Inches same value
        [Test]
        public void CompareInches_SameValue_ReturnsTrue()
        {
            bool result = QuantityMeasurementService.CompareInches(1.0, 1.0);

            // Correct NUnit assertion
            Assert.That(result, Is.True);
        }

        // UC2 Test: Inches different value
        [Test]
        public void CompareInches_DifferentValue_ReturnsFalse()
        {
            bool result = QuantityMeasurementService.CompareInches(1.0, 2.0);

            // Correct NUnit assertion
            Assert.That(result, Is.False);
        }
    }
}