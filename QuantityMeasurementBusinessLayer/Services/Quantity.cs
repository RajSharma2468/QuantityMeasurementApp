using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Enums;  // ADD THIS

namespace QuantityMeasurementBusinessLayer.Services
{
    public class Quantity : IMeasurable
    {
        private double _value;
        private string _unitType;
        private string _unitName;
        
        public Quantity(double value, string unitType, string unitName)
        {
            this._value = value;
            this._unitType = unitType;
            this._unitName = unitName;
        }
        
        public double ConvertToBaseUnit(double value)
        {
            if (this._unitType == "Length")
            {
                LengthUnit unit;
                if (System.Enum.TryParse<LengthUnit>(this._unitName, out unit))
                {
                    return unit.ToMeters(value);
                }
            }
            else if (this._unitType == "Weight")
            {
                WeightUnit unit;
                if (System.Enum.TryParse<WeightUnit>(this._unitName, out unit))
                {
                    return unit.ToGrams(value);
                }
            }
            else if (this._unitType == "Volume")
            {
                VolumeUnit unit;
                if (System.Enum.TryParse<VolumeUnit>(this._unitName, out unit))
                {
                    return unit.ToMilliliters(value);
                }
            }
            else if (this._unitType == "Temperature")
            {
                TemperatureUnit unit;
                if (System.Enum.TryParse<TemperatureUnit>(this._unitName, out unit))
                {
                    return unit.ToCelsius(value);
                }
            }
            
            return value;
        }
        
        public double ConvertFromBaseUnit(double baseValue)
        {
            if (this._unitType == "Length")
            {
                LengthUnit unit;
                if (System.Enum.TryParse<LengthUnit>(this._unitName, out unit))
                {
                    return unit.FromMeters(baseValue);
                }
            }
            else if (this._unitType == "Weight")
            {
                WeightUnit unit;
                if (System.Enum.TryParse<WeightUnit>(this._unitName, out unit))
                {
                    return unit.FromGrams(baseValue);
                }
            }
            else if (this._unitType == "Volume")
            {
                VolumeUnit unit;
                if (System.Enum.TryParse<VolumeUnit>(this._unitName, out unit))
                {
                    return unit.FromMilliliters(baseValue);
                }
            }
            else if (this._unitType == "Temperature")
            {
                TemperatureUnit unit;
                if (System.Enum.TryParse<TemperatureUnit>(this._unitName, out unit))
                {
                    return unit.FromCelsius(baseValue);
                }
            }
            
            return baseValue;
        }
        
        public string GetUnitName()
        {
            return this._unitName;
        }
    }
}