using System.Collections.Generic;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAll();
        List<QuantityMeasurementEntity> GetByOperation(string operation);
        List<QuantityMeasurementEntity> GetByUnitType(string unitType);
        int GetTotalCount();
        void DeleteAll();
        Dictionary<string, object> GetPoolStatistics();
        void ReleaseResources();
    }
}