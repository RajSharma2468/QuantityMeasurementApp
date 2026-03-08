using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.UnitTests
{
    public class LengthUnitTests
    {
        [Test]
        public void Inches_To_Feet()
        {
            double result = LengthUnit.INCHES.ConvertToBaseUnit(12);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Yards_To_Feet()
        {
            double result = LengthUnit.YARDS.ConvertToBaseUnit(1);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Cm_To_Feet()
        {
            double result = LengthUnit.CENTIMETERS.ConvertToBaseUnit(30.48);

            Assert.AreEqual(1, result, 0.01);
        }
    }
}