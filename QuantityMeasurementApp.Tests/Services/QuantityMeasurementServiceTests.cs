using NUnit.Framework;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementServiceTests
    {
        [Test]
        public void Add_Feet_And_Feet_Should_Return_Sum_In_Feet()
        {
            var q1 = new Quantity(2, LengthUnit.FEET);
            var q2 = new Quantity(3, LengthUnit.FEET);

            var result = q1.Add(q2);

            Assert.That(result.Value, Is.EqualTo(5).Within(0.001));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        [Test]
        public void Add_Feet_And_Inches_Should_Return_Correct_Result_In_Feet()
        {
            var q1 = new Quantity(1, LengthUnit.FEET);
            var q2 = new Quantity(12, LengthUnit.INCHES);

            var result = q1.Add(q2);

            Assert.That(result.Value, Is.EqualTo(2).Within(0.001));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        [Test]
        public void Add_Yards_And_Feet_Should_Return_Correct_Result_In_Yards()
        {
            var q1 = new Quantity(1, LengthUnit.YARDS); // 3 feet
            var q2 = new Quantity(3, LengthUnit.FEET);  // 3 feet

            var result = q1.Add(q2);

            Assert.That(result.Value, Is.EqualTo(2).Within(0.001));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.YARDS));
        }

        [Test]
        public void Add_Meter_And_Feet_Should_Return_Correct_Result_In_Meter()
        {
            var q1 = new Quantity(1, LengthUnit.METER);
            var q2 = new Quantity(3.28084, LengthUnit.FEET);

            var result = q1.Add(q2);

            Assert.That(result.Value, Is.EqualTo(2).Within(0.01));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.METER));
        }

        [Test]
        public void Add_With_Null_Should_Throw_Exception()
        {
            var q1 = new Quantity(1, LengthUnit.FEET);

            Assert.Throws<ArgumentNullException>(() => q1.Add(null));
        }
    }
}