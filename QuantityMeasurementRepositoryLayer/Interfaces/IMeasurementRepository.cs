using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IMeasurementRepository
    {
        Task SaveMeasurementAsync(Measurement measurement);
        Task<List<Measurement>> GetMeasurementHistoryAsync(int userId);
        Task<List<Measurement>> GetAllMeasurementsAsync();
        Task<Measurement?> GetMeasurementByIdAsync(int id);
        Task DeleteMeasurementAsync(int id);
        Task UpdateMeasurementAsync(Measurement measurement);
    }
}