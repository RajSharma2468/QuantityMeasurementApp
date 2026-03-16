using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementModelLayer.Models
{
    public class QuantityWeight : QuantityModel
    {
        public QuantityWeight(double value, WeightUnit unit)
        {
            this.Value = value;
            this.UnitType = "Weight";
            this.UnitName = unit.ToString();
        }
    }
}