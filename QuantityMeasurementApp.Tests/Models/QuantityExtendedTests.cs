using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityExtendedTests
    {
        [Test]
        public void Should_Compare_Meter_And_Inch()
        {
            var meter = new Quantity(1, LengthUnit.METER);
            var inch = new Quantity(39.37, LengthUnit.INCHES);

            Assert.That(meter == inch, Is.True);
        }
    }
}