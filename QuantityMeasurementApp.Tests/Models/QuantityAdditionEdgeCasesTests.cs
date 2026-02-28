using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class QuantityAdditionEdgeCasesTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void Add_ZeroValue()
        {
            var q1 = new QuantityLength(0.0, LengthUnit.FEET);
            var q2 = new QuantityLength(5.0, LengthUnit.FEET);

            var result = q1.Add(q2, LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(5.0).Within(Epsilon));
        }

        [Test]
        public void Add_NegativeValue()
        {
            var q1 = new QuantityLength(-3.0, LengthUnit.FEET);
            var q2 = new QuantityLength(5.0, LengthUnit.FEET);

            var result = q1.Add(q2, LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(Epsilon));
        }
    }
}