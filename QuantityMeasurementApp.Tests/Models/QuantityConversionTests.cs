using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class QuantityConversionTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void FeetToInches()
        {
            var q = new QuantityLength(2.0, LengthUnit.FEET);
            var converted = q.Add(new QuantityLength(0, LengthUnit.FEET), LengthUnit.INCHES);

            Assert.That(converted.Value, Is.EqualTo(24.0).Within(Epsilon));
            Assert.That(converted.Unit, Is.EqualTo(LengthUnit.INCHES));
        }

        [Test]
        public void InchesToFeet()
        {
            var q = new QuantityLength(24.0, LengthUnit.INCHES);
            var converted = q.Add(new QuantityLength(0, LengthUnit.INCHES), LengthUnit.FEET);

            Assert.That(converted.Value, Is.EqualTo(2.0).Within(Epsilon));
            Assert.That(converted.Unit, Is.EqualTo(LengthUnit.FEET));
        }
    }
}