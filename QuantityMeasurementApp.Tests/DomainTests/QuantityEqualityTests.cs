using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Units;

namespace QuantityMeasurementApp.Tests.DomainTests
{
    public class QuantityEqualityTests
    {
        [Test]
        public void Foot_Equals_Inch()
        {
            QuantityLength q1 = new QuantityLength(1, LengthUnit.FOOT);
            QuantityLength q2 = new QuantityLength(12, LengthUnit.INCH);

            Assert.IsTrue(q1.Equals(q2));
        }
    }
}