using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Exceptions;
using QuantityMeasurementRepositoryLayer.Interfaces;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Tests.ServiceTests
{
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private Mock<IQuantityMeasurementRepository> _mockRepository;
        private Mock<ILogger<QuantityMeasurementServiceImpl>> _mockLogger;
        private QuantityMeasurementServiceImpl _service;
        
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IQuantityMeasurementRepository>();
            _mockLogger = new Mock<ILogger<QuantityMeasurementServiceImpl>>();
            _service = new QuantityMeasurementServiceImpl(_mockRepository.Object, _mockLogger.Object);
        }
        
        [TestMethod]
        public void Add_SameUnitType_ShouldReturnResult()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityModel q2 = new QuantityLength(2, LengthUnit.Foot);
            
            // Act
            QuantityDTO result = _service.Add(q1, q2);
            
            // Assert
            Assert.AreEqual(7, result.Result);
            Assert.AreEqual("Add", result.Operation);
            _mockRepository.Verify(r => r.Save(It.IsAny<QuantityMeasurementEntity>()), Times.Once);
        }
        
        [TestMethod]
        public void Add_DifferentUnitTypes_ShouldThrowException()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityModel q2 = new QuantityWeight(2, WeightUnit.Kilogram);
            
            // Act & Assert
            Assert.ThrowsException<UnsupportedOperationException>(() => _service.Add(q1, q2));
        }
        
        [TestMethod]
        public void Subtract_SameUnitType_ShouldReturnResult()
        {
            // Arrange
            QuantityModel q1 = new QuantityWeight(10, WeightUnit.Kilogram);
            QuantityModel q2 = new QuantityWeight(3, WeightUnit.Kilogram);
            
            // Act
            QuantityDTO result = _service.Subtract(q1, q2);
            
            // Assert
            Assert.AreEqual(7, result.Result);
            Assert.AreEqual("Subtract", result.Operation);
            _mockRepository.Verify(r => r.Save(It.IsAny<QuantityMeasurementEntity>()), Times.Once);
        }
        
        [TestMethod]
        public void Subtract_DifferentUnitTypes_ShouldThrowException()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityModel q2 = new QuantityWeight(2, WeightUnit.Kilogram);
            
            // Act & Assert
            Assert.ThrowsException<UnsupportedOperationException>(() => _service.Subtract(q1, q2));
        }
        
        [TestMethod]
        public void Compare_EqualValues_ShouldReturnTrue()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(1, LengthUnit.Foot);
            QuantityModel q2 = new QuantityLength(12, LengthUnit.Inch);
            
            // Act
            bool result = _service.Compare(q1, q2);
            
            // Assert
            Assert.IsTrue(result);
            _mockRepository.Verify(r => r.Save(It.IsAny<QuantityMeasurementEntity>()), Times.Once);
        }
        
        [TestMethod]
        public void Compare_DifferentValues_ShouldReturnFalse()
        {
            // Arrange
            QuantityModel q1 = new QuantityLength(2, LengthUnit.Foot);
            QuantityModel q2 = new QuantityLength(12, LengthUnit.Inch);
            
            // Act
            bool result = _service.Compare(q1, q2);
            
            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void Convert_ValidUnits_ShouldReturnConvertedValue()
        {
            // Arrange
            QuantityModel from = new QuantityLength(10, LengthUnit.Foot);
            QuantityModel to = new QuantityLength(0, LengthUnit.Inch);
            
            // Act
            QuantityDTO result = _service.Convert(from, to);
            
            // Assert
            Assert.AreEqual(120, result.Result);
            Assert.AreEqual("Convert", result.Operation);
            _mockRepository.Verify(r => r.Save(It.IsAny<QuantityMeasurementEntity>()), Times.Once);
        }
        
        [TestMethod]
        public void GetAllMeasurements_ShouldReturnList()
        {
            // Arrange
            List<QuantityMeasurementEntity> entities = new List<QuantityMeasurementEntity>();
            
            QuantityMeasurementEntity e1 = new QuantityMeasurementEntity();
            e1.Id = 1;
            e1.Value = 10;
            e1.UnitType = "Length";
            e1.UnitName = "Foot";
            e1.Operation = "Add";
            entities.Add(e1);
            
            _mockRepository.Setup(r => r.GetAll()).Returns(entities);
            
            // Act
            List<QuantityDTO> result = _service.GetAllMeasurements();
            
            // Assert
            Assert.AreEqual(1, result.Count);
        }
        
        [TestMethod]
        public void GetTotalCount_ShouldReturnCount()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetTotalCount()).Returns(5);
            
            // Act
            int result = _service.GetTotalCount();
            
            // Assert
            Assert.AreEqual(5, result);
        }
        
        [TestMethod]
        public void DeleteAll_ShouldCallRepository()
        {
            // Act
            _service.DeleteAll();
            
            // Assert
            _mockRepository.Verify(r => r.DeleteAll(), Times.Once);
        }
    }
}