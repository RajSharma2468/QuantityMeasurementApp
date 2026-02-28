using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class InchTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void InchesToFeetConversion()
        {
            var q = new QuantityLength(12.0, LengthUnit.INCHES);
            var feet = q.Add(new QuantityLength(0, LengthUnit.INCHES), LengthUnit.FEET);

            Assert.That(feet.Value, Is.EqualTo(1.0).Within(Epsilon));
            Assert.That(feet.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        [Test]
        public void InchesToYardsConversion()
        {
            var q = new QuantityLength(36.0, LengthUnit.INCHES);
            var yards = q.Add(new QuantityLength(0, LengthUnit.INCHES), LengthUnit.YARDS);

            Assert.That(yards.Value, Is.EqualTo(1.0).Within(Epsilon));
            Assert.That(yards.Unit, Is.EqualTo(LengthUnit.YARDS));
        }

        [Test]
        public void InchesToCentimetersConversion()
        {
            var q = new QuantityLength(1.0, LengthUnit.INCHES);
            var cm = q.Add(new QuantityLength(0, LengthUnit.INCHES), LengthUnit.CENTIMETERS);

            Assert.That(cm.Value, Is.EqualTo(2.54).Within(Epsilon));
            Assert.That(cm.Unit, Is.EqualTo(LengthUnit.CENTIMETERS));
        }
    }
}