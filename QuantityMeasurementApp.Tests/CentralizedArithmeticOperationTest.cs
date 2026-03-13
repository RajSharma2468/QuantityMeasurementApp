using NUnit.Framework;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using ModelLayer.Models;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class CentralizedArithmeticOperationTest
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
        public void Service_AllOperations_WithLength_WorkCorrectly()
        {
            // Arrange
            var length1 = new QuantityDTO(10, "FEET", "Length");
            var length2 = new QuantityDTO(5, "FEET", "Length");

            // Act - Test all operations
            var compareResult = _service.Compare(length1, length2);
            var addResult = _service.Add(length1, length2);
            var subtractResult = _service.Subtract(length1, length2);
            var divideResult = _service.Divide(length1, length2);

            // Assert
            Assert.AreEqual(0, compareResult.Value); // Not equal
            Assert.AreEqual(15, addResult.Value);
            Assert.AreEqual(5, subtractResult.Value);
            Assert.AreEqual(2, divideResult.Value);
        }

        [Test]
        public void Service_AllOperations_WithWeight_WorkCorrectly()
        {
            // Arrange
            var weight1 = new QuantityDTO(1000, "GRAM", "Weight");
            var weight2 = new QuantityDTO(500, "GRAM", "Weight");

            // Act
            var compareResult = _service.Compare(weight1, weight2);
            var addResult = _service.Add(weight1, weight2);
            var subtractResult = _service.Subtract(weight1, weight2);
            var divideResult = _service.Divide(weight1, weight2);

            // Assert
            Assert.AreEqual(0, compareResult.Value);
            Assert.AreEqual(1500, addResult.Value);
            Assert.AreEqual(500, subtractResult.Value);
            Assert.AreEqual(2, divideResult.Value);
        }

        [Test]
        public void Service_AllOperations_WithVolume_WorkCorrectly()
        {
            // Arrange
            var vol1 = new QuantityDTO(2, "LITER", "Volume");
            var vol2 = new QuantityDTO(1000, "MILLILITER", "Volume");

            // Act
            var compareResult = _service.Compare(vol1, vol2);
            var addResult = _service.Add(vol1, vol2);
            var subtractResult = _service.Subtract(vol1, vol2);
            var divideResult = _service.Divide(vol1, vol2);

            // Assert
            Assert.AreEqual(0, compareResult.Value); // Not equal (2L vs 1L)
            Assert.AreEqual(3, addResult.Value); // 2L + 1000mL = 3L
            Assert.AreEqual(1, subtractResult.Value); // 2L - 1000mL = 1L
            Assert.AreEqual(2, divideResult.Value); // 2L / 1000mL = 2
        }

        [Test]
        public void Service_AllOperations_WithTemperature_CompareOnlyWorks()
        {
            // Arrange
            var temp1 = new QuantityDTO(100, "CELSIUS", "Temperature");
            var temp2 = new QuantityDTO(212, "FAHRENHEIT", "Temperature");

            // Act - Compare should work
            var compareResult = _service.Compare(temp1, temp2);

            // Assert
            Assert.AreEqual(1, compareResult.Value); // Equal temperatures
            
            // Other operations should throw
            Assert.Throws<ModelLayer.Exceptions.QuantityMeasurementException>(
                () => _service.Add(temp1, temp2));
            Assert.Throws<ModelLayer.Exceptions.QuantityMeasurementException>(
                () => _service.Subtract(temp1, temp2));
            Assert.Throws<ModelLayer.Exceptions.QuantityMeasurementException>(
                () => _service.Divide(temp1, temp2));
        }

        [Test]
        public void Repository_SavesAllOperations_CanBeRetrieved()
        {
            // Arrange
            var q1 = new QuantityDTO(10, "FEET", "Length");
            var q2 = new QuantityDTO(5, "FEET", "Length");

            // Act - Perform operations
            _service.Compare(q1, q2);
            _service.Add(q1, q2);
            _service.Convert(q1, "INCH");

            // Assert
            var history = _service.GetOperationHistory();
            Assert.AreEqual(3, history.Count);
        }

        [Test]
        public void CrossCategoryOperations_AllFail_Consistently()
        {
            // Arrange
            var length = new QuantityDTO(10, "FEET", "Length");
            var weight = new QuantityDTO(10, "GRAM", "Weight");

            // Assert - All operations throw same exception type
            Assert.Throws<ModelLayer.Exceptions.QuantityMeasurementException>(
                () => _service.Compare(length, weight));
            Assert.Throws<ModelLayer.Exceptions.QuantityMeasurementException>(
                () => _service.Add(length, weight));
            Assert.Throws<ModelLayer.Exceptions.QuantityMeasurementException>(
                () => _service.Subtract(length, weight));
            Assert.Throws<ModelLayer.Exceptions.QuantityMeasurementException>(
                () => _service.Divide(length, weight));
        }
    }
}