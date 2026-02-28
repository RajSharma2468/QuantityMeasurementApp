using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityAdditionTests
    {
        private const double EPSILON = 0.0001;

        [Test]
        public void FeetPlusFeet_ShouldReturnCorrectResult()
        {
            // Arrange
            Quantity q1 = new Quantity(1.0, LengthUnit.FEET);
            Quantity q2 = new Quantity(2.0, LengthUnit.FEET);

            // Act
            Quantity result = q1.Add(q2);

            // Assert
            Assert.That(result.Value, Is.EqualTo(3.0).Within(EPSILON));
        }

        [Test]
        public void FeetPlusInches_ShouldReturnCorrectResult()
        {
            // Arrange
            Quantity q1 = new Quantity(1.0, LengthUnit.FEET);
            Quantity q2 = new Quantity(12.0, LengthUnit.INCHES);

            // Act
            Quantity result = q1.Add(q2);

            // Assert
            Assert.That(result.Value, Is.EqualTo(2.0).Within(EPSILON));
        }
    }
}