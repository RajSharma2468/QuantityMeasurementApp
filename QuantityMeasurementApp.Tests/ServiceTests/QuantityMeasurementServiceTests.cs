using NUnit.Framework;
using QuantityMeasurementApp.Services;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.ServiceTests
{
    public class QuantityMeasurementServiceTests
    {
        [Test]
        public void Service_Conversion_Test()
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            var result = service.Convert(1, LengthUnit.FEET, LengthUnit.INCHES);

            Assert.AreEqual(12, result.Value);
        }

        [Test]
        public void Service_Addition_Test()
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            var result = service.Add(1, LengthUnit.FEET, 12, LengthUnit.INCHES, LengthUnit.FEET);

            Assert.AreEqual(2, result.Value);
        }
    }
}