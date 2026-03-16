using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Moq;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementRepositoryLayer.Interfaces;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Tests.IntegrationTests
{
    [TestClass]
    public class QuantityMeasurementIntegrationTests
    {
        private IQuantityMeasurementRepository _repository;
        private IQuantityMeasurementService _service;
        private Mock<ILogger<QuantityMeasurementCacheRepository>> _mockCacheLogger;
        private Mock<ILogger<QuantityMeasurementServiceImpl>> _mockServiceLogger;
        
        [TestInitialize]
        public void Setup()
        {
            _mockCacheLogger = new Mock<ILogger<QuantityMeasurementCacheRepository>>();
            _mockServiceLogger = new Mock<ILogger<QuantityMeasurementServiceImpl>>();
            
            _repository = new QuantityMeasurementCacheRepository(_mockCacheLogger.Object);
            _service = new QuantityMeasurementServiceImpl(_repository, _mockServiceLogger.Object);
        }
        
        [TestMethod]
        public void EndToEnd_AddOperation_ShouldSaveToRepository()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityModel q2 = new QuantityLength(2, LengthUnit.Foot);
            
            // Act
            QuantityDTO result = _service.Add(q1, q2);
            
            // Assert
            Assert.AreEqual(7, result.Result);
            
            List<QuantityDTO> all = _service.GetAllMeasurements();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual("Add", all[0].Operation);
        }
        
        [TestMethod]
        public void EndToEnd_MultipleOperations_ShouldSaveAll()
        {
            // Arrange
            QuantityModel len1 = new QuantityLength(10, LengthUnit.Foot);
            QuantityModel len2 = new QuantityLength(120, LengthUnit.Inch);
            
            QuantityModel weight1 = new QuantityWeight(5, WeightUnit.Kilogram);
            QuantityModel weight2 = new QuantityWeight(5000, WeightUnit.Gram);
            
            // Act
            _service.Add(len1, len2);
            _service.Compare(weight1, weight2);
            _service.Convert(len1, len2);
            
            // Assert
            List<QuantityDTO> all = _service.GetAllMeasurements();
            Assert.AreEqual(3, all.Count);
            
            List<QuantityDTO> addOps = _service.GetMeasurementsByOperation("Add");
            Assert.AreEqual(1, addOps.Count);
            
            List<QuantityDTO> compareOps = _service.GetMeasurementsByOperation("Compare");
            Assert.AreEqual(1, compareOps.Count);
            
            List<QuantityDTO> convertOps = _service.GetMeasurementsByOperation("Convert");
            Assert.AreEqual(1, convertOps.Count);
        }
        
        [TestMethod]
        public void EndToEnd_CompareEqualQuantities_ShouldReturnTrueAndSave()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(1, LengthUnit.Foot);
            QuantityModel q2 = new QuantityLength(12, LengthUnit.Inch);
            
            // Act
            bool areEqual = _service.Compare(q1, q2);
            
            // Assert
            Assert.IsTrue(areEqual);
            
            List<QuantityDTO> measurements = _service.GetAllMeasurements();
            Assert.AreEqual(1, measurements.Count);
            Assert.AreEqual("Compare", measurements[0].Operation);
        }
        
        [TestMethod]
        public void EndToEnd_ConvertTemperature_ShouldWorkCorrectly()
        {
            // Arrange
            QuantityModel from = new QuantityModel();
            from.Value = 100;
            from.UnitType = "Temperature";
            from.UnitName = "Celsius";
            
            QuantityModel to = new QuantityModel();
            to.Value = 0;
            to.UnitType = "Temperature";
            to.UnitName = "Fahrenheit";
            
            // Act
            QuantityDTO result = _service.Convert(from, to);
            
            // Assert
            Assert.AreEqual(212, result.Result);
            
            List<QuantityDTO> measurements = _service.GetAllMeasurements();
            Assert.AreEqual(1, measurements.Count);
            Assert.AreEqual("Convert", measurements[0].Operation);
        }
        
        [TestMethod]
        public void EndToEnd_GetAllMeasurements_ShouldReturnCorrectCount()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityModel q2 = new QuantityLength(2, LengthUnit.Foot);
            
            _service.Add(q1, q2);
            _service.Compare(q1, q2);
            
            // Act
            int totalCount = _service.GetTotalCount();
            
            // Assert
            Assert.AreEqual(2, totalCount);
        }
        
        [TestMethod]
        public void EndToEnd_DeleteAll_ShouldRemoveAllData()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityModel q2 = new QuantityLength(2, LengthUnit.Foot);
            
            _service.Add(q1, q2);
            Assert.AreEqual(1, _service.GetTotalCount());
            
            // Act
            _service.DeleteAll();
            
            // Assert
            Assert.AreEqual(0, _service.GetTotalCount());
        }
    }
}