using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Interfaces;

public interface IQuantityMeasurementService
{
    Task<QuantityResultDTO> CompareQuantitiesAsync(QuantityInputDTO input);
    Task<QuantityResultDTO> ConvertQuantitiesAsync(QuantityInputDTO input);
    Task<QuantityResultDTO> AddQuantitiesAsync(QuantityInputDTO input);
    Task<QuantityResultDTO> SubtractQuantitiesAsync(QuantityInputDTO input);
    Task<QuantityResultDTO> MultiplyQuantitiesAsync(QuantityInputDTO input);
    Task<QuantityResultDTO> DivideQuantitiesAsync(QuantityInputDTO input);
    
    Task<List<QuantityResultDTO>> GetOperationHistoryAsync(string operation);
    Task<List<QuantityResultDTO>> GetMeasurementsByTypeAsync(string measurementType);
    Task<long> GetOperationCountAsync(string operation);
    Task<List<QuantityResultDTO>> GetErrorHistoryAsync();
}