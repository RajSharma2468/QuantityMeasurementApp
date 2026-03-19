using QuantityMeasurementModelLayer.Enums;  

namespace QuantityMeasurementModelLayer.Models
{
    public class QuantityLength : QuantityModel
    {
        public QuantityLength(double value, LengthUnit unit)
        {
            this.Value = value;
            this.UnitType = "Length";
            this.UnitName = unit.ToString();
        }
    }
}