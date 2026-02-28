using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class QuantityConversionEdgeCasesTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void ConvertZeroFeetToInches()
        {
            var q = new QuantityLength(0.0, LengthUnit.FEET);
            var converted = q.Add(new QuantityLength(0, LengthUnit.FEET), LengthUnit.INCHES);

            Assert.That(converted.Value, Is.EqualTo(0.0).Within(Epsilon));
        }

        [Test]
        public void ConvertLargeValue()
        {
            var q = new QuantityLength(1000.0, LengthUnit.FEET);
            var converted = q.Add(new QuantityLength(0, LengthUnit.FEET), LengthUnit.INCHES);

            Assert.That(converted.Value, Is.EqualTo(12000.0).Within(Epsilon));
        }
    }
}