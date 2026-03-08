using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.QuantityTests
{
    public class QuantityArithmeticTests
    {
        [Test]
        public void Add_Feet_And_Inches()
        {
            Quantity q1 = new Quantity(1, LengthUnit.FEET);
            Quantity q2 = new Quantity(12, LengthUnit.INCHES);

            Quantity result = q1.Add(q2, LengthUnit.FEET);

            Assert.AreEqual(2, result.Value);
        }

        [Test]
        public void Add_Yard_And_Feet()
        {
            Quantity q1 = new Quantity(1, LengthUnit.YARDS);
            Quantity q2 = new Quantity(3, LengthUnit.FEET);

            Quantity result = q1.Add(q2, LengthUnit.YARDS);

            Assert.AreEqual(2, result.Value);
        }
    }
}