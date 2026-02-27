using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class LengthUnitTests
    {
        [Test]
        public void LengthUnit_EnumValues_ShouldExist()
        {
            Assert.That(LengthUnit.FEET.ToString(), Is.EqualTo("FEET"));
            Assert.That(LengthUnit.INCH.ToString(), Is.EqualTo("INCH"));
            Assert.That(LengthUnit.YARD.ToString(), Is.EqualTo("YARD"));
            Assert.That(LengthUnit.CENTIMETER.ToString(), Is.EqualTo("CENTIMETER"));
        }
    }
}