using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityConversionEdgeCasesTests
    {
        [Test]
        public void Should_Handle_Zero_Value()
        {
            var q1 = new Quantity(0, LengthUnit.FEET);
            var q2 = new Quantity(0, LengthUnit.INCHES);

            Assert.That(q1 == q2, Is.True);
        }
    }
}