using System;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementModelLayer.Entities
{
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public double? Result { get; set; }
        public DateTime OperationDate { get; set; }
        
        public QuantityMeasurementEntity()
        {
        }
        
        public QuantityMeasurementEntity(QuantityDTO dto)
        {
            this.Value = dto.Value;
            this.UnitType = dto.UnitType;
            this.UnitName = dto.UnitName;
            this.Operation = dto.Operation;
            this.Result = dto.Result;
            this.OperationDate = dto.OperationDate;
        }
        
        public QuantityDTO ToDTO()
        {
            QuantityDTO dto = new QuantityDTO();
            dto.Value = this.Value;
            dto.UnitType = this.UnitType;
            dto.UnitName = this.UnitName;
            dto.Operation = this.Operation;
            dto.Result = this.Result;
            dto.OperationDate = this.OperationDate;
            return dto;
        }
    }
}