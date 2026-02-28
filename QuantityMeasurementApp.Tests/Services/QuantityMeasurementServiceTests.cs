using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestFixture]
    public class QuantityMeasurementServiceTests
    {
        private const double Epsilon = 1e-3;
        private QuantityMeasurementService service;

        [SetUp]
        public void Setup()
        {
            service = new QuantityMeasurementService();
        }

        [Test]
        public void Service_AddTwoLengths()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.FEET);
            var q2 = new QuantityLength(12.0, LengthUnit.INCHES);

            var result = service.Add(q1, q2, LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(2.0).Within(Epsilon));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }
    }
}