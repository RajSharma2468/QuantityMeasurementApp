using NUnit.Framework;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class QuantityAdditionValidationTests
    {
        [Test]
        public void Add_NullOperand_ThrowsException()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);

            Assert.Throws<ArgumentNullException>(() => q1.Add(null, LengthUnit.FEET));
        }

        [Test]
        public void Add_InvalidTargetUnit_ThrowsException()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(2.0, LengthUnit.FEET);

            Assert.Throws<ArgumentException>(() => q1.Add(q2, (LengthUnit)(-1)));
        }
    }
}