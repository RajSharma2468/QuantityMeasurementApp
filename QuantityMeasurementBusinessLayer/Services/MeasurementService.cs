using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementBusinessLayer.Converters;
using QuantityMeasurementModelLayer.DTOs.Measurement;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepo;

        public MeasurementService(IMeasurementRepository measurementRepo)
        {
            _measurementRepo = measurementRepo;
        }

        public async Task<ConvertResponseDto> ConvertAsync(ConvertRequestDto request, int userId)
        {
            double result = 0;
            string formula = string.Empty;

            switch (request.Category.ToLower())
            {
                case "length":
                    result = LengthConverter.Convert(request.FromUnit, request.ToUnit, request.Value);
                    double rate = LengthConverter.GetConversionRate(request.FromUnit, request.ToUnit);
                    formula = $"1 {request.FromUnit} = {rate} {request.ToUnit}";
                    break;
                    
                case "weight":
                    result = WeightConverter.Convert(request.FromUnit, request.ToUnit, request.Value);
                    rate = WeightConverter.GetConversionRate(request.FromUnit, request.ToUnit);
                    formula = $"1 {request.FromUnit} = {rate} {request.ToUnit}";
                    break;
                    
                case "temperature":
                    result = TemperatureConverter.Convert(request.FromUnit, request.ToUnit, request.Value);
                    formula = $"{request.Value}°{request.FromUnit} = {result}°{request.ToUnit}";
                    break;
            }

            var measurement = new Measurement
            {
                UserId = userId,
                FromUnit = request.FromUnit,
                ToUnit = request.ToUnit,
                InputValue = request.Value,
                OutputValue = result,
                CreatedAt = DateTime.UtcNow
            };
            
            await _measurementRepo.SaveMeasurementAsync(measurement);

            return new ConvertResponseDto
            {
                InputValue = request.Value,
                FromUnit = request.FromUnit,
                OutputValue = result,
                ToUnit = request.ToUnit,
                Formula = formula
            };
        }

        public async Task<List<Measurement>> GetHistoryAsync(int userId)
        {
            return await _measurementRepo.GetMeasurementHistoryAsync(userId);
        }

        public async Task<List<Measurement>> GetAllHistoryAsync()
        {
            return await _measurementRepo.GetAllMeasurementsAsync();
        }

        public async Task<Measurement?> GetMeasurementByIdAsync(int id)
        {
            return await _measurementRepo.GetMeasurementByIdAsync(id);
        }

        public async Task<bool> DeleteMeasurementAsync(int id)
        {
            var measurement = await _measurementRepo.GetMeasurementByIdAsync(id);
            if (measurement == null) return false;
                
            await _measurementRepo.DeleteMeasurementAsync(id);
            return true;
        }

        // Fixed: Made this synchronous to avoid async warning
        public double GetConversionRate(string fromUnit, string toUnit)
        {
            if (LengthConverter.GetUnits().Contains(fromUnit) && LengthConverter.GetUnits().Contains(toUnit))
                return LengthConverter.GetConversionRate(fromUnit, toUnit);
                
            if (WeightConverter.GetUnits().Contains(fromUnit) && WeightConverter.GetUnits().Contains(toUnit))
                return WeightConverter.GetConversionRate(fromUnit, toUnit);
                
            if (TemperatureConverter.GetUnits().Contains(fromUnit) && TemperatureConverter.GetUnits().Contains(toUnit))
                return TemperatureConverter.GetConversionRate(fromUnit, toUnit);
                
            throw new ArgumentException($"Cannot convert between {fromUnit} and {toUnit}");
        }
    }
}