using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.ValueObjects;

namespace QuantityMeasurementApp.Tests.ValueObjectTests
{
    public class InchTests
    {
        [Test]
        public void Create_Inch_Object()
        {
            Inch inch = new Inch(10);

            Assert.AreEqual(10, inch.Value);
        }
    }
}