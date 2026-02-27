using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class InchTests
    {
        [Test]
        public void Inch_ObjectCreation_ValueShouldBeCorrect()
        {
            Inch inch = new Inch(10);
            Assert.That(inch.Value, Is.EqualTo(10));
            Assert.That(inch.Unit, Is.EqualTo(LengthUnit.INCH));
        }
    }
}