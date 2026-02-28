using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class QuantityAdditionTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void Add_SameUnits()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.FEET);
            var q2 = new QuantityLength(3.0, LengthUnit.FEET);

            var result = q1.Add(q2, LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(5.0).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        [Test]
        public void Add_DifferentUnits()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            var result = q1.Add(q2, LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }
    }
}