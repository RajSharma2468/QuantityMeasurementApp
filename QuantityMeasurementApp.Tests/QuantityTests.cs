using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityTests
    {
        [Test]
        public void Test_YardToFeet_Equal()
        {
            Quantity yard = new Quantity(1.0, LengthUnit.YARDS);
            Quantity feet = new Quantity(3.0, LengthUnit.FEET);

            Assert.IsTrue(yard.Compare(feet));
        }

        [Test]
        public void Test_CmToInch_Equal()
        {
            Quantity cm = new Quantity(1.0, LengthUnit.CENTIMETERS);
            Quantity inch = new Quantity(0.393701, LengthUnit.INCHES);

            Assert.IsTrue(cm.Compare(inch));
        }

        [Test]
        public void Test_NotEqual()
        {
            Quantity yard = new Quantity(1.0, LengthUnit.YARDS);
            Quantity feet = new Quantity(2.0, LengthUnit.FEET);

            Assert.IsFalse(yard.Compare(feet));
        }
    }
}