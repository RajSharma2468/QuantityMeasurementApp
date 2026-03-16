using System;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementModelLayer.Models
{
    public class QuantityModel
    {
        public double Value { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public string UnitName { get; set; } = string.Empty;
        
        public double ConvertTo(QuantityModel target)
        {
            if (this.UnitType != target.UnitType)
            {
                throw new InvalidOperationException("Cannot convert between different unit types: " + this.UnitType + " and " + target.UnitType);
            }
            
            if (this.UnitType == "Length")
            {
                return this.ConvertLength(target);
            }
            else if (this.UnitType == "Weight")
            {
                return this.ConvertWeight(target);
            }
            else if (this.UnitType == "Volume")
            {
                return this.ConvertVolume(target);
            }
            else if (this.UnitType == "Temperature")
            {
                return this.ConvertTemperature(target);
            }
            else
            {
                throw new ArgumentException("Unknown unit type: " + this.UnitType);
            }
        }
        
        private double ConvertLength(QuantityModel target)
        {
            LengthUnit fromUnit;
            LengthUnit toUnit;
            
            if (Enum.TryParse<LengthUnit>(this.UnitName, out fromUnit) && 
                Enum.TryParse<LengthUnit>(target.UnitName, out toUnit))
            {
                double meters = fromUnit.ToMeters(this.Value);
                return toUnit.FromMeters(meters);
            }
            else
            {
                throw new ArgumentException("Invalid length unit");
            }
        }
        
        private double ConvertWeight(QuantityModel target)
        {
            WeightUnit fromUnit;
            WeightUnit toUnit;
            
            if (Enum.TryParse<WeightUnit>(this.UnitName, out fromUnit) && 
                Enum.TryParse<WeightUnit>(target.UnitName, out toUnit))
            {
                double grams = fromUnit.ToGrams(this.Value);
                return toUnit.FromGrams(grams);
            }
            else
            {
                throw new ArgumentException("Invalid weight unit");
            }
        }
        
        private double ConvertVolume(QuantityModel target)
        {
            VolumeUnit fromUnit;
            VolumeUnit toUnit;
            
            if (Enum.TryParse<VolumeUnit>(this.UnitName, out fromUnit) && 
                Enum.TryParse<VolumeUnit>(target.UnitName, out toUnit))
            {
                double milliliters = fromUnit.ToMilliliters(this.Value);
                return toUnit.FromMilliliters(milliliters);
            }
            else
            {
                throw new ArgumentException("Invalid volume unit");
            }
        }
        
        private double ConvertTemperature(QuantityModel target)
        {
            TemperatureUnit fromUnit;
            TemperatureUnit toUnit;
            
            if (Enum.TryParse<TemperatureUnit>(this.UnitName, out fromUnit) && 
                Enum.TryParse<TemperatureUnit>(target.UnitName, out toUnit))
            {
                double celsius = fromUnit.ToCelsius(this.Value);
                return toUnit.FromCelsius(celsius);
            }
            else
            {
                throw new ArgumentException("Invalid temperature unit");
            }
        }
        
        public override string ToString()
        {
            string symbol = this.UnitName;
            
            if (this.UnitType == "Length")
            {
                LengthUnit len;
                if (Enum.TryParse<LengthUnit>(this.UnitName, out len))
                {
                    symbol = len.GetSymbol();
                }
            }
            else if (this.UnitType == "Weight")
            {
                WeightUnit wt;
                if (Enum.TryParse<WeightUnit>(this.UnitName, out wt))
                {
                    symbol = wt.GetSymbol();
                }
            }
            else if (this.UnitType == "Volume")
            {
                VolumeUnit vol;
                if (Enum.TryParse<VolumeUnit>(this.UnitName, out vol))
                {
                    symbol = vol.GetSymbol();
                }
            }
            else if (this.UnitType == "Temperature")
            {
                TemperatureUnit temp;
                if (Enum.TryParse<TemperatureUnit>(this.UnitName, out temp))
                {
                    symbol = temp.GetSymbol();
                }
            }
            
            return this.Value + " " + symbol;
        }
    }
}