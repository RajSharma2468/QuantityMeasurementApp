using System;
using QuantityMeasurementModelLayer.Models;  // Add this line

namespace QuantityMeasurementModelLayer.DTO
{
    public class QuantityDTO
    {
        public double Value { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public double? Result { get; set; }
        public DateTime OperationDate { get; set; }
        
        public QuantityDTO()
        {
        }
        
        public QuantityDTO(QuantityModel model, string operation, double? result)
        {
            this.Value = model.Value;
            this.UnitType = model.UnitType;
            this.UnitName = model.UnitName;
            this.Operation = operation;
            this.Result = result;
            this.OperationDate = DateTime.Now;
        }
    }
}