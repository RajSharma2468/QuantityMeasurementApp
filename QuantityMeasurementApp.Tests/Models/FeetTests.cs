using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class FeetTests
    {
        [Test]
        public void CreateFeet_ShouldStoreValueCorrectly()
        {
            Feet feet = new Feet(5.0);

            Assert.That(feet.Value, Is.EqualTo(5.0));
            Assert.That(feet.Unit, Is.EqualTo(LengthUnit.FEET));
        }
    }
}