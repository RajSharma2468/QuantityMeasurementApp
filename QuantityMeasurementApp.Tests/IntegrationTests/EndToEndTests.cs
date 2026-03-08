using NUnit.Framework;
using QuantityMeasurementApp.Services;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.IntegrationTests
{
    public class EndToEndTests
    {
        [Test]
        public void End_To_End_Conversion()
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            var result = service.Convert(2, LengthUnit.FEET, LengthUnit.INCHES);

            Assert.AreEqual(24, result.Value);
        }
    }
}