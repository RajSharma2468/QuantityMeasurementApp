using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityConversionTests
    {
        [Test]
        public void Should_Compare_1Feet_And_12Inch_As_Equal()
        {
            var feet = new Quantity(1, LengthUnit.FEET);
            var inch = new Quantity(12, LengthUnit.INCHES);

            Assert.That(feet == inch, Is.True);
        }
    }
}