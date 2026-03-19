using Microsoft.Extensions.Logging;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Interfaces;
using System.Text.Json;

namespace QuantityMeasurementBusinessLayer.Services;

public class QuantityMeasurementService : IQuantityMeasurementService
{
    private readonly IQuantityMeasurementRepository _repository;
    private readonly ILogger<QuantityMeasurementService> _logger;
    
    public QuantityMeasurementService(
        IQuantityMeasurementRepository repository,
        ILogger<QuantityMeasurementService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<QuantityResultDTO> CompareQuantitiesAsync(QuantityInputDTO input)
    {
        try
        {
            _logger.LogInformation("Comparing quantities: {Input}", JsonSerializer.Serialize(input));
            
            // Validate measurement types
            if (input.ThisQuantity.MeasurementType != input.ThatQuantity.MeasurementType)
            {
                throw new InvalidOperationException(
                    $"Cannot compare different measurement categories: " +
                    $"{input.ThisQuantity.MeasurementType} and {input.ThatQuantity.MeasurementType}");
            }
            
            // Perform comparison
            var thisInBaseUnit = ConvertToBaseUnit(
                input.ThisQuantity.Value, 
                input.ThisQuantity.Unit, 
                input.ThisQuantity.MeasurementType);
                
            var thatInBaseUnit = ConvertToBaseUnit(
                input.ThatQuantity.Value, 
                input.ThatQuantity.Unit, 
                input.ThatQuantity.MeasurementType);
            
            bool isEqual = Math.Abs(thisInBaseUnit - thatInBaseUnit) < 0.0001;
            
            // Save to repository
            var entity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Compare.ToString().ToLower(),
                ResultString = isEqual.ToString().ToLower(),
                IsError = false
            };
            
            var savedEntity = await _repository.AddAsync(entity);
            
            return MapToResultDTO(savedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error comparing quantities");
            
            // Save error to repository
            var errorEntity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Compare.ToString().ToLower(),
                ErrorMessage = ex.Message,
                IsError = true
            };
            
            await _repository.AddAsync(errorEntity);
            throw;
        }
    }
    
    public async Task<QuantityResultDTO> ConvertQuantitiesAsync(QuantityInputDTO input)
    {
        try
        {
            _logger.LogInformation("Converting quantities: {Input}", JsonSerializer.Serialize(input));
            
            // Validate measurement types
            if (input.ThisQuantity.MeasurementType != input.ThatQuantity.MeasurementType)
            {
                throw new InvalidOperationException(
                    $"Cannot convert between different measurement categories: " +
                    $"{input.ThisQuantity.MeasurementType} and {input.ThatQuantity.MeasurementType}");
            }
            
            // Perform conversion
            var valueInBaseUnit = ConvertToBaseUnit(
                input.ThisQuantity.Value, 
                input.ThisQuantity.Unit, 
                input.ThisQuantity.MeasurementType);
                
            var convertedValue = ConvertFromBaseUnit(
                valueInBaseUnit, 
                input.ThatQuantity.Unit, 
                input.ThatQuantity.MeasurementType);
            
            // Save to repository
            var entity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Convert.ToString().ToLower(),
                ResultValue = convertedValue,
                ResultUnit = input.ThatQuantity.Unit,
                ResultMeasurementType = input.ThatQuantity.MeasurementType,
                IsError = false
            };
            
            var savedEntity = await _repository.AddAsync(entity);
            
            return MapToResultDTO(savedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error converting quantities");
            
            // Save error to repository
            var errorEntity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Convert.ToString().ToLower(),
                ErrorMessage = ex.Message,
                IsError = true
            };
            
            await _repository.AddAsync(errorEntity);
            throw;
        }
    }
    
    public async Task<QuantityResultDTO> AddQuantitiesAsync(QuantityInputDTO input)
    {
        try
        {
            _logger.LogInformation("Adding quantities: {Input}", JsonSerializer.Serialize(input));
            
            // Validate measurement types
            if (input.ThisQuantity.MeasurementType != input.ThatQuantity.MeasurementType)
            {
                throw new InvalidOperationException(
                    $"Cannot add different measurement categories: " +
                    $"{input.ThisQuantity.MeasurementType} and {input.ThatQuantity.MeasurementType}");
            }
            
            // Perform addition
            var thisInBaseUnit = ConvertToBaseUnit(
                input.ThisQuantity.Value, 
                input.ThisQuantity.Unit, 
                input.ThisQuantity.MeasurementType);
                
            var thatInBaseUnit = ConvertToBaseUnit(
                input.ThatQuantity.Value, 
                input.ThatQuantity.Unit, 
                input.ThatQuantity.MeasurementType);
            
            double sum = thisInBaseUnit + thatInBaseUnit;
            
            // Convert back to original unit
            var resultInOriginalUnit = ConvertFromBaseUnit(
                sum, 
                input.ThisQuantity.Unit, 
                input.ThisQuantity.MeasurementType);
            
            // Save to repository
            var entity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Add.ToString().ToLower(),
                ResultValue = resultInOriginalUnit,
                ResultUnit = input.ThisQuantity.Unit,
                ResultMeasurementType = input.ThisQuantity.MeasurementType,
                IsError = false
            };
            
            var savedEntity = await _repository.AddAsync(entity);
            
            return MapToResultDTO(savedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding quantities");
            
            // Save error to repository
            var errorEntity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Add.ToString().ToLower(),
                ErrorMessage = ex.Message,
                IsError = true
            };
            
            await _repository.AddAsync(errorEntity);
            throw;
        }
    }
    
    public async Task<QuantityResultDTO> SubtractQuantitiesAsync(QuantityInputDTO input)
    {
        try
        {
            _logger.LogInformation("Subtracting quantities: {Input}", JsonSerializer.Serialize(input));
            
            // Validate measurement types
            if (input.ThisQuantity.MeasurementType != input.ThatQuantity.MeasurementType)
            {
                throw new InvalidOperationException(
                    $"Cannot subtract different measurement categories: " +
                    $"{input.ThisQuantity.MeasurementType} and {input.ThatQuantity.MeasurementType}");
            }
            
            // Perform subtraction
            var thisInBaseUnit = ConvertToBaseUnit(
                input.ThisQuantity.Value, 
                input.ThisQuantity.Unit, 
                input.ThisQuantity.MeasurementType);
                
            var thatInBaseUnit = ConvertToBaseUnit(
                input.ThatQuantity.Value, 
                input.ThatQuantity.Unit, 
                input.ThatQuantity.MeasurementType);
            
            double difference = thisInBaseUnit - thatInBaseUnit;
            
            // Convert back to original unit
            var resultInOriginalUnit = ConvertFromBaseUnit(
                difference, 
                input.ThisQuantity.Unit, 
                input.ThisQuantity.MeasurementType);
            
            // Save to repository
            var entity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Subtract.ToString().ToLower(),
                ResultValue = resultInOriginalUnit,
                ResultUnit = input.ThisQuantity.Unit,
                ResultMeasurementType = input.ThisQuantity.MeasurementType,
                IsError = false
            };
            
            var savedEntity = await _repository.AddAsync(entity);
            
            return MapToResultDTO(savedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error subtracting quantities");
            
            // Save error to repository
            var errorEntity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Subtract.ToString().ToLower(),
                ErrorMessage = ex.Message,
                IsError = true
            };
            
            await _repository.AddAsync(errorEntity);
            throw;
        }
    }
    
    public async Task<QuantityResultDTO> MultiplyQuantitiesAsync(QuantityInputDTO input)
    {
        try
        {
            _logger.LogInformation("Multiplying quantities: {Input}", JsonSerializer.Serialize(input));
            
            // Perform multiplication (can be between different types)
            var result = input.ThisQuantity.Value * input.ThatQuantity.Value;
            
            // Save to repository
            var entity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Multiply.ToString().ToLower(),
                ResultValue = result,
                ResultUnit = input.ThisQuantity.Unit,
                ResultMeasurementType = input.ThisQuantity.MeasurementType,
                IsError = false
            };
            
            var savedEntity = await _repository.AddAsync(entity);
            
            return MapToResultDTO(savedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error multiplying quantities");
            
            // Save error to repository
            var errorEntity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Multiply.ToString().ToLower(),
                ErrorMessage = ex.Message,
                IsError = true
            };
            
            await _repository.AddAsync(errorEntity);
            throw;
        }
    }
    
    public async Task<QuantityResultDTO> DivideQuantitiesAsync(QuantityInputDTO input)
    {
        try
        {
            _logger.LogInformation("Dividing quantities: {Input}", JsonSerializer.Serialize(input));
            
            // Check for division by zero
            if (input.ThatQuantity.Value == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            
            // Perform division
            var result = input.ThisQuantity.Value / input.ThatQuantity.Value;
            
            // Save to repository
            var entity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Divide.ToString().ToLower(),
                ResultValue = result,
                ResultUnit = input.ThisQuantity.Unit,
                ResultMeasurementType = input.ThisQuantity.MeasurementType,
                IsError = false
            };
            
            var savedEntity = await _repository.AddAsync(entity);
            
            return MapToResultDTO(savedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dividing quantities");
            
            // Save error to repository
            var errorEntity = new QuantityMeasurement
            {
                ThisValue = input.ThisQuantity.Value,
                ThisUnit = input.ThisQuantity.Unit,
                ThisMeasurementType = input.ThisQuantity.MeasurementType,
                ThatValue = input.ThatQuantity.Value,
                ThatUnit = input.ThatQuantity.Unit,
                ThatMeasurementType = input.ThatQuantity.MeasurementType,
                Operation = OperationType.Divide.ToString().ToLower(),
                ErrorMessage = ex.Message,
                IsError = true
            };
            
            await _repository.AddAsync(errorEntity);
            throw;
        }
    }
    
    public async Task<List<QuantityResultDTO>> GetOperationHistoryAsync(string operation)
    {
        var entities = await _repository.GetByOperationAsync(operation);
        return entities.Select(MapToResultDTO).ToList();
    }
    
    public async Task<List<QuantityResultDTO>> GetMeasurementsByTypeAsync(string measurementType)
    {
        var entities = await _repository.GetByMeasurementTypeAsync(measurementType);
        return entities.Select(MapToResultDTO).ToList();
    }
    
    public async Task<long> GetOperationCountAsync(string operation)
    {
        return await _repository.CountByOperationAsync(operation);
    }
    
    public async Task<List<QuantityResultDTO>> GetErrorHistoryAsync()
    {
        var entities = await _repository.GetErrorsAsync();
        return entities.Select(MapToResultDTO).ToList();
    }
    
    #region Private Helper Methods
    
    private double ConvertToBaseUnit(double value, string unit, string measurementType)
    {
        return measurementType.ToLower() switch
        {
            "lengthunit" => UnitConversionFactor.LengthToInch.ContainsKey(unit) 
                ? value * UnitConversionFactor.LengthToInch[unit] 
                : value,
            "weightunit" => UnitConversionFactor.WeightToGram.ContainsKey(unit)
                ? value * UnitConversionFactor.WeightToGram[unit]
                : value,
            "volumeunit" => UnitConversionFactor.VolumeToMilliliter.ContainsKey(unit)
                ? value * UnitConversionFactor.VolumeToMilliliter[unit]
                : value,
            "temperatureunit" => ConvertTemperatureToCelsius(value, unit),
            _ => value
        };
    }
    
    private double ConvertFromBaseUnit(double value, string unit, string measurementType)
    {
        return measurementType.ToLower() switch
        {
            "lengthunit" => UnitConversionFactor.LengthToInch.ContainsKey(unit)
                ? value / UnitConversionFactor.LengthToInch[unit]
                : value,
            "weightunit" => UnitConversionFactor.WeightToGram.ContainsKey(unit)
                ? value / UnitConversionFactor.WeightToGram[unit]
                : value,
            "volumeunit" => UnitConversionFactor.VolumeToMilliliter.ContainsKey(unit)
                ? value / UnitConversionFactor.VolumeToMilliliter[unit]
                : value,
            "temperatureunit" => ConvertCelsiusToTemperature(value, unit),
            _ => value
        };
    }
    
    private double ConvertTemperatureToCelsius(double value, string unit)
    {
        return unit.ToLower() switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32) * 5 / 9,
            "kelvin" => value - 273.15,
            _ => value
        };
    }
    
    private double ConvertCelsiusToTemperature(double value, string unit)
    {
        return unit.ToLower() switch
        {
            "celsius" => value,
            "fahrenheit" => (value * 9 / 5) + 32,
            "kelvin" => value + 273.15,
            _ => value
        };
    }
    
    private QuantityResultDTO MapToResultDTO(QuantityMeasurement entity)
    {
        return new QuantityResultDTO
        {
            ThisValue = entity.ThisValue,
            ThisUnit = entity.ThisUnit,
            ThisMeasurementType = entity.ThisMeasurementType,
            ThatValue = entity.ThatValue,
            ThatUnit = entity.ThatUnit,
            ThatMeasurementType = entity.ThatMeasurementType,
            Operation = entity.Operation,
            ResultString = entity.ResultString,
            ResultValue = entity.ResultValue,
            ResultUnit = entity.ResultUnit,
            ResultMeasurementType = entity.ResultMeasurementType,
            ErrorMessage = entity.ErrorMessage,
            IsError = entity.IsError,
            CreatedAt = entity.CreatedAt
        };
    }
    
    #endregion
}