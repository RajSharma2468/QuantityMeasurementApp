using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.ValueObjects;

namespace QuantityMeasurementApp.Tests.ValueObjectTests
{
    public class FeetTests
    {
        [Test]
        public void Create_Feet_Object()
        {
            Feet feet = new Feet(5);

            Assert.AreEqual(5, feet.Value);
        }
    }
}