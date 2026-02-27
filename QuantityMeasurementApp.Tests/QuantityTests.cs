using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityTests
    {
        [Test]
        public void EqualLengths_FeetAndInch_ReturnTrue()
        {
            Quantity q1 = new Quantity(1, LengthUnit.FEET);
            Quantity q2 = new Quantity(12, LengthUnit.INCH);

            Assert.That(q1.Equals(q2), Is.True);
        }

        [Test]
        public void DifferentLengths_ReturnFalse()
        {
            Quantity q1 = new Quantity(1, LengthUnit.FEET);
            Quantity q2 = new Quantity(10, LengthUnit.INCH);

            Assert.That(q1.Equals(q2), Is.False);
        }
    }
}