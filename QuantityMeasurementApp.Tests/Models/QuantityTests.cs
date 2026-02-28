using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityTests
    {
        [Test]
        public void Should_Return_True_For_Same_Feet()
        {
            var q1 = new Quantity(1, LengthUnit.FEET);
            var q2 = new Quantity(1, LengthUnit.FEET);

            Assert.That(q1 == q2, Is.True);
        }

        [Test]
        public void Should_Return_False_For_Different_Feet()
        {
            var q1 = new Quantity(1, LengthUnit.FEET);
            var q2 = new Quantity(2, LengthUnit.FEET);

            Assert.That(q1 == q2, Is.False);
        }
    }
}