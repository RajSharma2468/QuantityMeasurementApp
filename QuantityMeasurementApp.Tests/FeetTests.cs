using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class FeetTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void FeetToInchesConversion()
        {
            var q = new QuantityLength(1.0, LengthUnit.FEET);
            // Use Add result directly, do not wrap in new QuantityLength
            var inches = q.Add(new QuantityLength(0, LengthUnit.FEET), LengthUnit.INCHES);

            Assert.That(inches.Value, Is.EqualTo(12.0).Within(Epsilon));
            Assert.That(inches.Unit, Is.EqualTo(LengthUnit.INCHES));
        }

        [Test]
        public void FeetToYardsConversion()
        {
            var q = new QuantityLength(3.0, LengthUnit.FEET);
            var yards = q.Add(new QuantityLength(0, LengthUnit.FEET), LengthUnit.YARDS);

            Assert.That(yards.Value, Is.EqualTo(1.0).Within(Epsilon));
            Assert.That(yards.Unit, Is.EqualTo(LengthUnit.YARDS));
        }

        [Test]
        public void FeetToCentimetersConversion()
        {
            var q = new QuantityLength(1.0, LengthUnit.FEET);
            var cm = q.Add(new QuantityLength(0, LengthUnit.FEET), LengthUnit.CENTIMETERS);

            Assert.That(cm.Value, Is.EqualTo(30.48).Within(Epsilon));
            Assert.That(cm.Unit, Is.EqualTo(LengthUnit.CENTIMETERS));
        }
    }
}