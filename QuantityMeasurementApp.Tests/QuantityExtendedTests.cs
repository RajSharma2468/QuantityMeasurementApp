using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityExtendedTests
    {
        private const double EPSILON = 0.0001;

        [Test]
        public void RoundTripConversion_ShouldPreserveValue()
        {
            Quantity original = new Quantity(5, LengthUnit.FEET);

            Quantity inches = original.ConvertTo(LengthUnit.INCH);
            Quantity backToFeet = inches.ConvertTo(LengthUnit.FEET);

            Assert.That(backToFeet.Value, Is.EqualTo(5).Within(EPSILON));
        }

        [Test]
        public void SameUnitConversion_ShouldReturnSameValue()
        {
            Quantity q = new Quantity(10, LengthUnit.FEET);
            Quantity result = q.ConvertTo(LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(10));
        }

        [Test]
        public void CentimeterToInch_ShouldConvertCorrectly()
        {
            Quantity q = new Quantity(2.54, LengthUnit.CENTIMETER);
            Quantity result = q.ConvertTo(LengthUnit.INCH);

            Assert.That(result.Value, Is.EqualTo(1).Within(EPSILON));
        }
    }
}