using ModelLayer.Interfaces;

namespace ModelLayer.Models
{
    public class QuantityDTO
    {
        public double Value { get; set; }
        public string UnitName { get; set; }
        public string MeasurementType { get; set; }

        public QuantityDTO()
        {
        }

        public QuantityDTO(double value, string unitName, string measurementType)
        {
            Value = value;
            UnitName = unitName;
            MeasurementType = measurementType;
        }

        public override string ToString()
        {
            return $"{Value} {UnitName} ({MeasurementType})";
        }
    }
}