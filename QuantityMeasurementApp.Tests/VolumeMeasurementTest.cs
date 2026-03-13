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
    public class VolumeMeasurementTest
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
        public void Convert_LiterToMilliliter_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "LITER", "Volume");

            // Act
            var result = _service.Convert(q1, "MILLILITER");

            // Assert
            Assert.AreEqual(1000, result.Value);
            Assert.AreEqual("MILLILITER", result.UnitName);
        }

        [Test]
        public void Convert_MilliliterToLiter_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(1000, "MILLILITER", "Volume");

            // Act
            var result = _service.Convert(q1, "LITER");

            // Assert
            Assert.AreEqual(1, result.Value);
            Assert.AreEqual("LITER", result.UnitName);
        }

        [Test]
        public void Convert_LiterToGallon_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "LITER", "Volume");

            // Act
            var result = _service.Convert(q1, "GALLON");

            // Assert
            Assert.AreEqual(0.264172, result.Value, 0.0001);
            Assert.AreEqual("GALLON", result.UnitName);
        }

        [Test]
        public void Convert_GallonToLiter_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "GALLON", "Volume");

            // Act
            var result = _service.Convert(q1, "LITER");

            // Assert
            Assert.AreEqual(3.78541, result.Value, 0.0001);
            Assert.AreEqual("LITER", result.UnitName);
        }

        [Test]
        public void Compare_Volume_SameValue_ReturnsTrue()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "LITER", "Volume");
            var q2 = new QuantityDTO(1000, "MILLILITER", "Volume");

            // Act
            var result = _service.Compare(q1, q2);

            // Assert
            Assert.AreEqual(1, result.Value);
        }

        [Test]
        public void Compare_Volume_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var q1 = new QuantityDTO(2, "LITER", "Volume");
            var q2 = new QuantityDTO(1000, "MILLILITER", "Volume");

            // Act
            var result = _service.Compare(q1, q2);

            // Assert
            Assert.AreEqual(0, result.Value);
        }

        [Test]
        public void Add_Volume_SameUnit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(2, "LITER", "Volume");
            var q2 = new QuantityDTO(3, "LITER", "Volume");

            // Act
            var result = _service.Add(q1, q2);

            // Assert
            Assert.AreEqual(5, result.Value);
            Assert.AreEqual("LITER", result.UnitName);
        }

        [Test]
        public void Add_Volume_DifferentUnits_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "LITER", "Volume");
            var q2 = new QuantityDTO(500, "MILLILITER", "Volume");

            // Act
            var result = _service.Add(q1, q2);

            // Assert
            Assert.AreEqual(1.5, result.Value);
            Assert.AreEqual("LITER", result.UnitName);
        }

        [Test]
        public void Subtract_Volume_SameUnit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(5, "LITER", "Volume");
            var q2 = new QuantityDTO(2, "LITER", "Volume");

            // Act
            var result = _service.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(3, result.Value);
            Assert.AreEqual("LITER", result.UnitName);
        }

        [Test]
        public void Subtract_Volume_DifferentUnits_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new QuantityDTO(2, "LITER", "Volume");
            var q2 = new QuantityDTO(500, "MILLILITER", "Volume");

            // Act
            var result = _service.Subtract(q1, q2);

            // Assert
            Assert.AreEqual(1.5, result.Value);
            Assert.AreEqual("LITER", result.UnitName);
        }

        [Test]
        public void Divide_Volume_SameUnit_ReturnsCorrectRatio()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "LITER", "Volume");
            var q2 = new QuantityDTO(2, "LITER", "Volume");

            // Act
            var result = _service.Divide(q1, q2);

            // Assert
            Assert.AreEqual(5, result.Value);
        }

        [Test]
        public void Divide_Volume_DifferentUnits_ReturnsCorrectRatio()
        {
            // Arrange
            var q1 = new QuantityDTO(2000, "MILLILITER", "Volume");
            var q2 = new QuantityDTO(1, "LITER", "Volume");

            // Act
            var result = _service.Divide(q1, q2);

            // Assert
            Assert.AreEqual(2, result.Value);
        }

        [Test]
        public void Add_VolumeWithDifferentType_ThrowsException()
        {
            // Arrange
            var q1 = new QuantityDTO(1, "LITER", "Volume");
            var q2 = new QuantityDTO(100, "GRAM", "Weight");

            // Act & Assert
            Assert.Throws<QuantityMeasurementException>(() => _service.Add(q1, q2));
        }

        [Test]
        public void Volume_AllOperations_WithDifferentUnits_WorkCorrectly()
        {
            // Arrange
            var vol1 = new QuantityDTO(2, "LITER", "Volume");
            var vol2 = new QuantityDTO(1500, "MILLILITER", "Volume");

            // Act
            var compareResult = _service.Compare(vol1, vol2);
            var addResult = _service.Add(vol1, vol2);
            var subtractResult = _service.Subtract(vol1, vol2);
            var divideResult = _service.Divide(vol1, vol2);

            // Assert
            Assert.AreEqual(0, compareResult.Value); // 2L vs 1.5L = not equal
            Assert.AreEqual(3.5, addResult.Value);  // 2L + 1.5L = 3.5L
            Assert.AreEqual(0.5, subtractResult.Value); // 2L - 1.5L = 0.5L
            Assert.AreEqual(1.333, divideResult.Value, 0.001); // 2L / 1.5L = 1.333
        }
    }
}