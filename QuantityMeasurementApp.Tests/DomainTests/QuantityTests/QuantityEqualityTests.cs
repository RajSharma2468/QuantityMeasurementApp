using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.QuantityTests
{
    public class QuantityEqualityTests
    {
        [Test]
        public void Same_Value_Same_Unit_Should_Be_Equal()
        {
            Quantity q1 = new Quantity(5, LengthUnit.FEET);
            Quantity q2 = new Quantity(5, LengthUnit.FEET);

            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Feet_And_Inches_Should_Be_Equal()
        {
            Quantity q1 = new Quantity(1, LengthUnit.FEET);
            Quantity q2 = new Quantity(12, LengthUnit.INCHES);

            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Inches_And_Yards_Should_Be_Equal()
        {
            Quantity q1 = new Quantity(36, LengthUnit.INCHES);
            Quantity q2 = new Quantity(1, LengthUnit.YARDS);

            Assert.IsTrue(q1.Equals(q2));
        }
    }
}