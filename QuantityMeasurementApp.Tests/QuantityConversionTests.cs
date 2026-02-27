using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityConversionTests
    {
        private const double EPSILON = 0.0001;

        [Test]
        public void FeetToInch_ShouldConvertCorrectly()
        {
            Quantity q = new Quantity(1, LengthUnit.FEET);
            Quantity result = q.ConvertTo(LengthUnit.INCH);

            Assert.That(result.Value, Is.EqualTo(12).Within(EPSILON));
        }

        [Test]
        public void InchToFeet_ShouldConvertCorrectly()
        {
            Quantity q = new Quantity(24, LengthUnit.INCH);
            Quantity result = q.ConvertTo(LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(2).Within(EPSILON));
        }

        [Test]
        public void YardToFeet_ShouldConvertCorrectly()
        {
            Quantity q = new Quantity(2, LengthUnit.YARD);
            Quantity result = q.ConvertTo(LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(6).Within(EPSILON));
        }
    }
}