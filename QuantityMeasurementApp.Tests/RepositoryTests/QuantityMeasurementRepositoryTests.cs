using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Tests.RepositoryTests
{
    [TestClass]
    public class QuantityMeasurementRepositoryTests
    {
        private Mock<ILogger<QuantityMeasurementCacheRepository>> _mockCacheLogger;
        private QuantityMeasurementCacheRepository _cacheRepository;
        
        [TestInitialize]
        public void Setup()
        {
            _mockCacheLogger = new Mock<ILogger<QuantityMeasurementCacheRepository>>();
            _cacheRepository = new QuantityMeasurementCacheRepository(_mockCacheLogger.Object);
        }
        
        [TestMethod]
        public void CacheRepository_Save_ShouldAddEntity()
        {
            // Arrange
            QuantityMeasurementEntity entity = new QuantityMeasurementEntity();
            entity.Value = 10;
            entity.UnitType = "Length";
            entity.UnitName = "Foot";
            entity.Operation = "Add";
            entity.OperationDate = DateTime.Now;
            
            // Act
            _cacheRepository.Save(entity);
            int count = _cacheRepository.GetTotalCount();
            
            // Assert
            Assert.AreEqual(1, count);
            Assert.IsTrue(entity.Id > 0);
        }
        
        [TestMethod]
        public void CacheRepository_GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            QuantityMeasurementEntity e1 = new QuantityMeasurementEntity();
            e1.Value = 10;
            e1.UnitType = "Length";
            e1.UnitName = "Foot";
            e1.Operation = "Add";
            e1.OperationDate = DateTime.Now;
            
            QuantityMeasurementEntity e2 = new QuantityMeasurementEntity();
            e2.Value = 20;
            e2.UnitType = "Weight";
            e2.UnitName = "Kilogram";
            e2.Operation = "Compare";
            e2.OperationDate = DateTime.Now;
            
            _cacheRepository.Save(e1);
            _cacheRepository.Save(e2);
            
            // Act
            List<QuantityMeasurementEntity> result = _cacheRepository.GetAll();
            
            // Assert
            Assert.AreEqual(2, result.Count);
        }
        
        [TestMethod]
        public void CacheRepository_GetByOperation_ShouldReturnFilteredEntities()
        {
            // Arrange
            QuantityMeasurementEntity e1 = new QuantityMeasurementEntity();
            e1.Value = 10;
            e1.UnitType = "Length";
            e1.UnitName = "Foot";
            e1.Operation = "Add";
            e1.OperationDate = DateTime.Now;
            
            QuantityMeasurementEntity e2 = new QuantityMeasurementEntity();
            e2.Value = 20;
            e2.UnitType = "Length";
            e2.UnitName = "Foot";
            e2.Operation = "Subtract";
            e2.OperationDate = DateTime.Now;
            
            _cacheRepository.Save(e1);
            _cacheRepository.Save(e2);
            
            // Act
            List<QuantityMeasurementEntity> addResults = _cacheRepository.GetByOperation("Add");
            List<QuantityMeasurementEntity> subtractResults = _cacheRepository.GetByOperation("Subtract");
            
            // Assert
            Assert.AreEqual(1, addResults.Count);
            Assert.AreEqual(1, subtractResults.Count);
        }
        
        [TestMethod]
        public void CacheRepository_GetByUnitType_ShouldReturnFilteredEntities()
        {
            // Arrange
            QuantityMeasurementEntity e1 = new QuantityMeasurementEntity();
            e1.Value = 10;
            e1.UnitType = "Length";
            e1.UnitName = "Foot";
            e1.Operation = "Add";
            e1.OperationDate = DateTime.Now;
            
            QuantityMeasurementEntity e2 = new QuantityMeasurementEntity();
            e2.Value = 20;
            e2.UnitType = "Weight";
            e2.UnitName = "Kilogram";
            e2.Operation = "Add";
            e2.OperationDate = DateTime.Now;
            
            _cacheRepository.Save(e1);
            _cacheRepository.Save(e2);
            
            // Act
            List<QuantityMeasurementEntity> lengthResults = _cacheRepository.GetByUnitType("Length");
            List<QuantityMeasurementEntity> weightResults = _cacheRepository.GetByUnitType("Weight");
            
            // Assert
            Assert.AreEqual(1, lengthResults.Count);
            Assert.AreEqual(1, weightResults.Count);
        }
        
        [TestMethod]
        public void CacheRepository_GetTotalCount_ShouldReturnCorrectNumber()
        {
            // Arrange
            QuantityMeasurementEntity e1 = new QuantityMeasurementEntity();
            e1.Value = 10;
            e1.UnitType = "Length";
            e1.UnitName = "Foot";
            e1.Operation = "Add";
            e1.OperationDate = DateTime.Now;
            
            QuantityMeasurementEntity e2 = new QuantityMeasurementEntity();
            e2.Value = 20;
            e2.UnitType = "Length";
            e2.UnitName = "Foot";
            e2.Operation = "Add";
            e2.OperationDate = DateTime.Now;
            
            _cacheRepository.Save(e1);
            _cacheRepository.Save(e2);
            
            // Act
            int count = _cacheRepository.GetTotalCount();
            
            // Assert
            Assert.AreEqual(2, count);
        }
        
        [TestMethod]
        public void CacheRepository_DeleteAll_ShouldRemoveAllEntities()
        {
            // Arrange
            QuantityMeasurementEntity e1 = new QuantityMeasurementEntity();
            e1.Value = 10;
            e1.UnitType = "Length";
            e1.UnitName = "Foot";
            e1.Operation = "Add";
            e1.OperationDate = DateTime.Now;
            
            _cacheRepository.Save(e1);
            Assert.AreEqual(1, _cacheRepository.GetTotalCount());
            
            // Act
            _cacheRepository.DeleteAll();
            int count = _cacheRepository.GetTotalCount();
            
            // Assert
            Assert.AreEqual(0, count);
        }
    }
}