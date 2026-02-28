using NUnit.Framework;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestFixture]
    public class QuantityAdditionExplicitTargetTests
    {
        private const double Epsilon = 1e-3;

        [Test]
        public void Add_ExplicitTargetUnit_Feet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(12.0, LengthUnit.INCHES);
            var result = q1.Add(q2, LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        [Test]
        public void Add_ExplicitTargetUnit_Inches()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(12.0, LengthUnit.INCHES);
            var result = q1.Add(q2, LengthUnit.INCHES);

            Assert.That(result.Value, Is.EqualTo(24.0).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.INCHES));
        }

        [Test]
        public void Add_ExplicitTargetUnit_Yards()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(12.0, LengthUnit.INCHES);
            var result = q1.Add(q2, LengthUnit.YARDS);

            Assert.That(result.Value, Is.EqualTo(0.667).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.YARDS));
        }

        [Test]
        public void Add_ExplicitTargetUnit_Centimeters()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.INCHES);
            var q2 = new QuantityLength(1.0, LengthUnit.INCHES);
            var result = q1.Add(q2, LengthUnit.CENTIMETERS);

            Assert.That(result.Value, Is.EqualTo(5.08).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.CENTIMETERS));
        }

        [Test]
        public void Add_NullTargetUnit_ThrowsException()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            Assert.Throws<ArgumentException>(() => q1.Add(q2, (LengthUnit)(-1)));
        }
    }
}