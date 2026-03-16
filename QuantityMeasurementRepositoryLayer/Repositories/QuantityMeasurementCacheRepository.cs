using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private List<QuantityMeasurementEntity> _measurements;
        private ILogger<QuantityMeasurementCacheRepository> _logger;
        
        public QuantityMeasurementCacheRepository(ILogger<QuantityMeasurementCacheRepository> logger)
        {
            this._measurements = new List<QuantityMeasurementEntity>();
            this._logger = logger;
            this._logger.LogInformation("CacheRepository started");
        }
        
        public void Save(QuantityMeasurementEntity entity)
        {
            entity.Id = this._measurements.Count + 1;
            this._measurements.Add(entity);
            this._logger.LogInformation("Saved measurement " + entity.Id);
        }
        
        public List<QuantityMeasurementEntity> GetAll()
        {
            List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();
            foreach (QuantityMeasurementEntity item in this._measurements)
            {
                result.Add(item);
            }
            return result;
        }
        
        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();
            foreach (QuantityMeasurementEntity item in this._measurements)
            {
                if (item.Operation == operation)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        
        public List<QuantityMeasurementEntity> GetByUnitType(string unitType)
        {
            List<QuantityMeasurementEntity> result = new List<QuantityMeasurementEntity>();
            foreach (QuantityMeasurementEntity item in this._measurements)
            {
                if (item.UnitType == unitType)
                {
                    result.Add(item);
                }
            }
            return result;
        }
        
        public int GetTotalCount()
        {
            return this._measurements.Count;
        }
        
        public void DeleteAll()
        {
            this._measurements.Clear();
            this._logger.LogInformation("Cleared all measurements");
        }
        
        public Dictionary<string, object> GetPoolStatistics()
        {
            Dictionary<string, object> stats = new Dictionary<string, object>();
            stats.Add("Repository Type", "Cache");
            stats.Add("Total Measurements", this._measurements.Count);
            return stats;
        }
        
        public void ReleaseResources()
        {
            this._measurements.Clear();
            this._logger.LogInformation("Released cache resources");
        }
    }
}