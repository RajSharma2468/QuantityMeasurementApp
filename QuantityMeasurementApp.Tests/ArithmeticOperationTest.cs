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
    public class ArithmeticOperationTest
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
        public void Add_Length_SameUnit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "FEET", "Length");
            var q2 = new QuantityDTO(3, "FEET", "Length");

            // Act
            var result = _service.Add(q1, q2);

            // Assert
            Assert.AreEqual(8, result.Value);
            Assert.AreEqual("FEET", result.UnitName);
        }

        [Test]
        public void Add_Length_DifferentUnits_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act
            var result = _service.Add(q1, q2);

            // Assert
            Assert.AreEqual(2, result.Value);
            Assert.AreEqual("FEET", result.UnitName);
        }

        [Test]
        public void Add_Weight_SameUnit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(100, "GRAM", "Weight");
            var q2 = new QuantityDTO(200, "GRAM", "Weight");

            // Act
            var result = _service.Add(q1, q2);

            // Assert
            Assert.AreEqual(300, result.Value);
            Assert.AreEqual("GRAM", result.UnitName);
        }

        [Test]
        public void Add_DifferentMeasurementTypes_ThrowsException()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "FEET", "Length");
            var q2 = new QuantityDTO(10, "GRAM", "Weight");

            // Act & Assert
            Assert.Throws<QuantityMeasurementException>(() => _service.Add(q1, q2));
        }

        [Test]
        public void Subtract_Length_SameUnit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(3, "FEET", "Length");

            // Act
            var result = _service.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(7, result.Value);
            Assert.AreEqual("FEET", result.UnitName);
        }

        [Test]
        public void Subtract_Length_DifferentUnits_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(2, "FEET", "Length");
            var q2 = new QuantityDTO(12, "INCH", "Length");

            // Act
            var result = _service.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(1, result.Value);
            Assert.AreEqual("FEET", result.UnitName);
        }

        [Test]
        public void Divide_Length_SameUnit_ReturnsCorrectRatio()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            var result = _service.Divide(q1, q2);

            // Assert
            Assert.AreEqual(5, result.Value);
        }

        [Test]
        public void Divide_Length_DifferentUnits_ReturnsCorrectRatio()
        {
            // Arrange
            var q1 = new QuantityDTO(24, "INCH", "Length");
            var q2 = new QuantityDTO(2, "FEET", "Length");

            // Act
            var result = _service.Divide(q1, q2);

            // Assert
            Assert.AreEqual(1, result.Value);
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(0, "FEET", "Length");

            // Act & Assert
            Assert.Throws<QuantityMeasurementException>(() => _service.Divide(q1, q2));
        }
    }
}

