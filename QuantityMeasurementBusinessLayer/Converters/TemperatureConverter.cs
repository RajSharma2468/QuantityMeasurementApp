using System;

namespace QuantityMeasurementBusinessLayer.Converters
{
    public static class TemperatureConverter
    {
        public static double Convert(string from, string to, double value)
        {
            // Convert to Celsius first
            double inCelsius = from switch
            {
                "Celsius" => value,
                "Fahrenheit" => (value - 32) * 5 / 9,
                "Kelvin" => value - 273.15,
                _ => throw new ArgumentException($"Invalid temperature unit: {from}")
            };

            // Convert from Celsius to target
            double result = to switch
            {
                "Celsius" => inCelsius,
                "Fahrenheit" => (inCelsius * 9 / 5) + 32,
                "Kelvin" => inCelsius + 273.15,
                _ => throw new ArgumentException($"Invalid temperature unit: {to}")
            };

            return Math.Round(result, 2);
        }

        public static double GetConversionRate(string from, string to)
        {
            // For temperature, conversion rate is not linear for all scales
            // Return the conversion for 1 unit
            if (from == to) return 1;
            
            if (from == "Celsius" && to == "Fahrenheit") return 33.8;
            if (from == "Celsius" && to == "Kelvin") return 274.15;
            if (from == "Fahrenheit" && to == "Celsius") return -17.2222;
            if (from == "Fahrenheit" && to == "Kelvin") return 255.928;
            if (from == "Kelvin" && to == "Celsius") return -272.15;
            if (from == "Kelvin" && to == "Fahrenheit") return -457.87;
            
            return Convert(from, to, 1);
        }

        public static List<string> GetUnits()
        {
            return new List<string> { "Celsius", "Fahrenheit", "Kelvin" };
        }
    }
}