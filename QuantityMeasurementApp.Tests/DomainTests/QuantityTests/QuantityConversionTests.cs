using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.QuantityTests
{
    public class QuantityConversionTests
    {
        [Test]
        public void Feet_To_Inches()
        {
            Quantity q = new Quantity(1, LengthUnit.FEET);

            Quantity result = q.ConvertTo(LengthUnit.INCHES);

            Assert.AreEqual(12, result.Value);
        }

        [Test]
        public void Inches_To_Feet()
        {
            Quantity q = new Quantity(24, LengthUnit.INCHES);

            Quantity result = q.ConvertTo(LengthUnit.FEET);

            Assert.AreEqual(2, result.Value);
        }
    }
}