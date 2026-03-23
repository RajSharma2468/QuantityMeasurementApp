using QuantityMeasurementModelLayer.DTOs.Measurement;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IMeasurementService
    {
        Task<ConvertResponseDto> ConvertAsync(ConvertRequestDto request, int userId);
        Task<List<Measurement>> GetHistoryAsync(int userId);
        Task<List<Measurement>> GetAllHistoryAsync();
        Task<Measurement?> GetMeasurementByIdAsync(int id);
        Task<bool> DeleteMeasurementAsync(int id);
        double GetConversionRate(string fromUnit, string toUnit); // Synchronous method
    }
}