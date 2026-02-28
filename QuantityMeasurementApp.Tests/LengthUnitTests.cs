using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class LengthUnitTests
    {
        [Test]
        public void LengthUnitEnumContainsAllUnits()
        {
            Assert.That(Enum.IsDefined(typeof(LengthUnit), "FEET"));
            Assert.That(Enum.IsDefined(typeof(LengthUnit), "INCHES"));
            Assert.That(Enum.IsDefined(typeof(LengthUnit), "YARDS"));
            Assert.That(Enum.IsDefined(typeof(LengthUnit), "CENTIMETERS"));
        }
    }
}