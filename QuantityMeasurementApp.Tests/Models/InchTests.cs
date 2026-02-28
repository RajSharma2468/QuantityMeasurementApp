using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class InchTests
    {
        [Test]
        public void CreateInch_ShouldStoreValueCorrectly()
        {
            // Arrange
            Inch inch = new Inch(12.0);

            // Assert
            Assert.That(inch.Value, Is.EqualTo(12.0));
            Assert.That(inch.Unit, Is.EqualTo(LengthUnit.INCHES));
        }
    }
}