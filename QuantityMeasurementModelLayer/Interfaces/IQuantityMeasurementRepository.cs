using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementModelLayer.Interfaces;

public interface IQuantityMeasurementRepository
{
    Task<QuantityMeasurement> AddAsync(QuantityMeasurement entity);
    Task<List<QuantityMeasurement>> GetAllAsync();
    Task<QuantityMeasurement?> GetByIdAsync(long id);
    Task<List<QuantityMeasurement>> GetByOperationAsync(string operation);
    Task<List<QuantityMeasurement>> GetByMeasurementTypeAsync(string measurementType);
    Task<List<QuantityMeasurement>> GetErrorsAsync();
    Task<long> CountByOperationAsync(string operation);
    Task<bool> UpdateAsync(QuantityMeasurement entity);
    Task<bool> DeleteAsync(long id);
}