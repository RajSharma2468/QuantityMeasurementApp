using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementModelLayer.Models
{
    public class QuantityVolume : QuantityModel
    {
        public QuantityVolume(double value, VolumeUnit unit)
        {
            this.Value = value;
            this.UnitType = "Volume";
            this.UnitName = unit.ToString();
        }
    }
}