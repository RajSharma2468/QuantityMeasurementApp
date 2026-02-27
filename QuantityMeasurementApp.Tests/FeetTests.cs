using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class FeetTests
    {
        [Test]
        public void Feet_ObjectCreation_ValueShouldBeCorrect()
        {
            Feet feet = new Feet(5);
            Assert.That(feet.Value, Is.EqualTo(5));
            Assert.That(feet.Unit, Is.EqualTo(LengthUnit.FEET));
        }
    }
}