using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestFixture]
    public class QuantityMeasurementServiceTests
    {
        private QuantityMeasurementService service;

        [SetUp]
        public void Setup()
        {
            service = new QuantityMeasurementService();
        }

        // Test for comparing equal feet values
        [Test]
        public void CompareFeet_WhenValuesAreEqual_ShouldReturnTrue()
        {
            Feet f1 = new Feet(3);
            Feet f2 = new Feet(3);

            bool result = service.CompareFeet(f1, f2);

            Assert.That(result, Is.True, "Feet values 3 and 3 should be equal");
        }

        // Test for adding two feet values
        [Test]
        public void AddFeet_ShouldReturnCorrectSum()
        {
            Feet f1 = new Feet(2);
            Feet f2 = new Feet(3);

            double result = service.AddFeet(f1, f2);

            Assert.That(result, Is.EqualTo(5), "2 feet + 3 feet should equal 5 feet");
        }

        // Test for feet to inches conversion
        [Test]
        public void ConvertFeetToInches_ShouldReturnCorrectValue()
        {
            Feet f = new Feet(1);

            double result = service.ConvertFeetToInches(f);

            Assert.That(result, Is.EqualTo(12), "1 foot should equal 12 inches");
        }
    }
}