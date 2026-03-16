using System;

namespace QuantityMeasurementModelLayer.Enums
{
    public static class TemperatureUnitExtensions
    {
        public static double ToCelsius(this TemperatureUnit unit, double value)
        {
            if (unit == TemperatureUnit.Celsius)
            {
                return value;
            }
            else if (unit == TemperatureUnit.Fahrenheit)
            {
                return (value - 32) * 5.0 / 9.0;
            }
            else if (unit == TemperatureUnit.Kelvin)
            {
                return value - 273.15;
            }
            else
            {
                throw new ArgumentException("Unknown temperature unit: " + unit);
            }
        }
        
        public static double FromCelsius(this TemperatureUnit unit, double celsius)
        {
            if (unit == TemperatureUnit.Celsius)
            {
                return celsius;
            }
            else if (unit == TemperatureUnit.Fahrenheit)
            {
                return (celsius * 9.0 / 5.0) + 32;
            }
            else if (unit == TemperatureUnit.Kelvin)
            {
                return celsius + 273.15;
            }
            else
            {
                throw new ArgumentException("Unknown temperature unit: " + unit);
            }
        }
        
        public static string GetSymbol(this TemperatureUnit unit)
        {
            if (unit == TemperatureUnit.Celsius)
            {
                return "°C";
            }
            else if (unit == TemperatureUnit.Fahrenheit)
            {
                return "°F";
            }
            else if (unit == TemperatureUnit.Kelvin)
            {
                return "K";
            }
            else
            {
                return unit.ToString();
            }
        }
    }
}