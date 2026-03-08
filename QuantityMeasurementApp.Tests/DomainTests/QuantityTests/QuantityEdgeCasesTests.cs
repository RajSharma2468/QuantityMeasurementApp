using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Tests.DomainTests.QuantityTests
{
    public class QuantityEdgeCasesTests
    {
        [Test]
        public void Invalid_Value_Should_Throw_Exception()
        {
            Assert.Throws<InvalidValueException>(() =>
                new Quantity(double.NaN, LengthUnit.FEET));
        }
    }
}