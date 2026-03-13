using NUnit.Framework;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using ModelLayer.Exceptions;
using ModelLayer.Models;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class TemperatureTest
    {
        private IQuantityMeasurementService _service;
        private IQuantityRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = QuantityRepository.Instance;
            _repository.Clear();
            _service = new QuantityMeasurementService(_repository);
        }

        [Test]
        public void Convert_CelsiusToFahrenheit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(100, "CELSIUS", "Temperature");

            // Act
            var result = _service.Convert(q1, "FAHRENHEIT");

            // Assert
            Assert.AreEqual(212, result.Value, 0.001);
            Assert.AreEqual("FAHRENHEIT", result.UnitName);
        }

        [Test]
        public void Convert_FahrenheitToCelsius_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act
            var result = _service.Convert(q1, "CELSIUS");

            // Assert
            Assert.AreEqual(0, result.Value, 0.001);
            Assert.AreEqual("CELSIUS", result.UnitName);
        }

        [Test]
        public void Convert_CelsiusToKelvin_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(0, "CELSIUS", "Temperature");

            // Act
            var result = _service.Convert(q1, "KELVIN");

            // Assert
            Assert.AreEqual(273.15, result.Value, 0.001);
            Assert.AreEqual("KELVIN", result.UnitName);
        }

        [Test]
        public void Convert_KelvinToCelsius_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            var result = _service.Convert(q1, "CELSIUS");

            // Assert
            Assert.AreEqual(0, result.Value, 0.001);
            Assert.AreEqual("CELSIUS", result.UnitName);
        }

        [Test]
        public void Compare_Temperature_SameValue_ReturnsTrue()
        {
            // Arrange
            var q1 = new QuantityDTO(100, "CELSIUS", "Temperature");
            var q2 = new QuantityDTO(212, "FAHRENHEIT", "Temperature");

            // Act
            var result = _service.Compare(q1, q2);

            // Assert
            Assert.AreEqual(1, result.Value);
        }

        [Test]
        public void Compare_Temperature_DifferentValue_ReturnsFalse()
        {
            // Arrange
            var q1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var q2 = new QuantityDTO(100, "FAHRENHEIT", "Temperature");

            // Act
            var result = _service.Compare(q1, q2);

            // Assert
            Assert.AreEqual(0, result.Value);
        }

        [Test]
        public void Add_Temperature_ThrowsException()
        {
            // Arrange
            var q1 = new QuantityDTO(100, "CELSIUS", "Temperature");
            var q2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act & Assert
            Assert.Throws<QuantityMeasurementException>(() => _service.Add(q1, q2));
        }

        [Test]
        public void Subtract_Temperature_ThrowsException()
        {
            // Arrange
            var q1 = new QuantityDTO(100, "CELSIUS", "Temperature");
            var q2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act & Assert
            Assert.Throws<QuantityMeasurementException>(() => _service.Subtract(q1, q2));
        }

        [Test]
        public void Divide_Temperature_ThrowsException()
        {
            // Arrange
            var q1 = new QuantityDTO(100, "CELSIUS", "Temperature");
            var q2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");

            // Act & Assert
            Assert.Throws<QuantityMeasurementException>(() => _service.Divide(q1, q2));
        }

        [Test]
        public void Convert_InvalidTargetUnit_ThrowsException()
        {
            // Arrange
            var q1 = new QuantityDTO(100, "CELSIUS", "Temperature");

            // Act & Assert
            Assert.Throws<QuantityMeasurementException>(() => _service.Convert(q1, "INVALID_UNIT"));
        }

        [Test]
        public void Temperature_Equality_WithDifferentUnits_WorksCorrectly()
        {
            // Arrange
            var temp1 = new QuantityDTO(0, "CELSIUS", "Temperature");
            var temp2 = new QuantityDTO(32, "FAHRENHEIT", "Temperature");
            var temp3 = new QuantityDTO(273.15, "KELVIN", "Temperature");

            // Act
            var result1 = _service.Compare(temp1, temp2);
            var result2 = _service.Compare(temp1, temp3);

            // Assert
            Assert.AreEqual(1, result1.Value);
            Assert.AreEqual(1, result2.Value);
        }
    }
}