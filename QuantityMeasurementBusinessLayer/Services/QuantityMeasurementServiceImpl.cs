using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Exceptions;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Enums;  // ADD THIS LINE
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private IQuantityMeasurementRepository _repository;
        private ILogger<QuantityMeasurementServiceImpl> _logger;
        
        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository, ILogger<QuantityMeasurementServiceImpl> logger)
        {
            this._repository = repository;
            this._logger = logger;
            this._logger.LogInformation("QuantityMeasurementService started");
        }
        
        public QuantityDTO Add(QuantityModel q1, QuantityModel q2)
        {
            try
            {
                this._logger.LogInformation("Adding " + q1.ToString() + " + " + q2.ToString());
                
                if (q1.UnitType != q2.UnitType)
                {
                    throw new UnsupportedOperationException("Cannot add different unit types: " + q1.UnitType + " and " + q2.UnitType);
                }
                
                double q2Converted = q2.ConvertTo(q1);
                double result = q1.Value + q2Converted;
                
                QuantityDTO dto = new QuantityDTO(q1, "Add", result);
                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(dto);
                
                this._repository.Save(entity);
                this._logger.LogInformation("Add result: " + result + " " + q1.UnitName);
                
                dto.Result = result;
                return dto;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error in Add");
                throw;
            }
        }
        
        public QuantityDTO Subtract(QuantityModel q1, QuantityModel q2)
        {
            try
            {
                this._logger.LogInformation("Subtracting " + q1.ToString() + " - " + q2.ToString());
                
                if (q1.UnitType != q2.UnitType)
                {
                    throw new UnsupportedOperationException("Cannot subtract different unit types: " + q1.UnitType + " and " + q2.UnitType);
                }
                
                double q2Converted = q2.ConvertTo(q1);
                double result = q1.Value - q2Converted;
                
                QuantityDTO dto = new QuantityDTO(q1, "Subtract", result);
                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(dto);
                
                this._repository.Save(entity);
                this._logger.LogInformation("Subtract result: " + result + " " + q1.UnitName);
                
                dto.Result = result;
                return dto;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error in Subtract");
                throw;
            }
        }
        
        public bool Compare(QuantityModel q1, QuantityModel q2)
        {
            try
            {
                this._logger.LogInformation("Comparing " + q1.ToString() + " and " + q2.ToString());
                
                if (q1.UnitType != q2.UnitType)
                {
                    throw new UnsupportedOperationException("Cannot compare different unit types: " + q1.UnitType + " and " + q2.UnitType);
                }
                
                double q1Base = 0;
                double q2Base = 0;
                
                if (q1.UnitType == "Length")
                {
                    LengthUnit len1, len2;
                    if (Enum.TryParse<LengthUnit>(q1.UnitName, out len1) && 
                        Enum.TryParse<LengthUnit>(q2.UnitName, out len2))
                    {
                        q1Base = len1.ToMeters(q1.Value);
                        q2Base = len2.ToMeters(q2.Value);
                    }
                }
                else if (q1.UnitType == "Weight")
                {
                    WeightUnit wt1, wt2;
                    if (Enum.TryParse<WeightUnit>(q1.UnitName, out wt1) && 
                        Enum.TryParse<WeightUnit>(q2.UnitName, out wt2))
                    {
                        q1Base = wt1.ToGrams(q1.Value);
                        q2Base = wt2.ToGrams(q2.Value);
                    }
                }
                else if (q1.UnitType == "Volume")
                {
                    VolumeUnit vol1, vol2;
                    if (Enum.TryParse<VolumeUnit>(q1.UnitName, out vol1) && 
                        Enum.TryParse<VolumeUnit>(q2.UnitName, out vol2))
                    {
                        q1Base = vol1.ToMilliliters(q1.Value);
                        q2Base = vol2.ToMilliliters(q2.Value);
                    }
                }
                else if (q1.UnitType == "Temperature")
                {
                    TemperatureUnit temp1, temp2;
                    if (Enum.TryParse<TemperatureUnit>(q1.UnitName, out temp1) && 
                        Enum.TryParse<TemperatureUnit>(q2.UnitName, out temp2))
                    {
                        q1Base = temp1.ToCelsius(q1.Value);
                        q2Base = temp2.ToCelsius(q2.Value);
                    }
                }
                
                double difference = q1Base - q2Base;
                if (difference < 0) difference = -difference;
                
                bool result = difference < 0.000001;
                
                QuantityDTO dto = new QuantityDTO(q1, "Compare", result ? 1 : 0);
                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(dto);
                
                this._repository.Save(entity);
                this._logger.LogInformation("Compare result: " + result);
                
                return result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error in Compare");
                throw;
            }
        }
        
        public QuantityDTO Convert(QuantityModel from, QuantityModel to)
        {
            try
            {
                this._logger.LogInformation("Converting " + from.ToString() + " to " + to.UnitName);
                
                double result = from.ConvertTo(to);
                
                QuantityDTO dto = new QuantityDTO(from, "Convert", result);
                QuantityMeasurementEntity entity = new QuantityMeasurementEntity(dto);
                
                this._repository.Save(entity);
                this._logger.LogInformation("Convert result: " + result + " " + to.UnitName);
                
                dto.Result = result;
                return dto;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error in Convert");
                throw;
            }
        }
        
        public List<QuantityDTO> GetAllMeasurements()
        {
            try
            {
                List<QuantityMeasurementEntity> entities = this._repository.GetAll();
                List<QuantityDTO> dtos = new List<QuantityDTO>();
                
                foreach (QuantityMeasurementEntity entity in entities)
                {
                    dtos.Add(entity.ToDTO());
                }
                
                return dtos;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error getting all measurements");
                throw;
            }
        }
        
        public List<QuantityDTO> GetMeasurementsByOperation(string operation)
        {
            try
            {
                List<QuantityMeasurementEntity> entities = this._repository.GetByOperation(operation);
                List<QuantityDTO> dtos = new List<QuantityDTO>();
                
                foreach (QuantityMeasurementEntity entity in entities)
                {
                    dtos.Add(entity.ToDTO());
                }
                
                return dtos;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error getting measurements by operation");
                throw;
            }
        }
        
        public List<QuantityDTO> GetMeasurementsByUnitType(string unitType)
        {
            try
            {
                List<QuantityMeasurementEntity> entities = this._repository.GetByUnitType(unitType);
                List<QuantityDTO> dtos = new List<QuantityDTO>();
                
                foreach (QuantityMeasurementEntity entity in entities)
                {
                    dtos.Add(entity.ToDTO());
                }
                
                return dtos;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error getting measurements by unit type");
                throw;
            }
        }
        
        public int GetTotalCount()
        {
            try
            {
                return this._repository.GetTotalCount();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error getting total count");
                throw;
            }
        }
        
        public void DeleteAll()
        {
            try
            {
                this._repository.DeleteAll();
                this._logger.LogInformation("Deleted all measurements");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error deleting all measurements");
                throw;
            }
        }
    }
}