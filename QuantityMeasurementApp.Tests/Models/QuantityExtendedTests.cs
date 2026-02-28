using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class QuantityExtendedTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void CommutativityTest()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            var res1 = q1.Add(q2, LengthUnit.YARDS);
            var res2 = q2.Add(q1, LengthUnit.YARDS);

            Assert.That(res1.Value, Is.EqualTo(res2.Value).Within(Epsilon));
            Assert.That(res1.Unit, Is.EqualTo(res2.Unit));
        }
    }
}