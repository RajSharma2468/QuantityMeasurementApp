using System;
using System.Collections.Generic;

namespace QuantityMeasurementBusinessLayer.Converters
{
    public static class LengthConverter
    {
        // Base unit: Meter
        private static readonly Dictionary<string, double> ToMeter = new Dictionary<string, double>
        {
            { "Meter", 1 },
            { "Kilometer", 1000 },
            { "Centimeter", 0.01 },
            { "Millimeter", 0.001 },
            { "Mile", 1609.34 },
            { "Yard", 0.9144 },
            { "Foot", 0.3048 },
            { "Inch", 0.0254 }
        };

        public static double Convert(string from, string to, double value)
        {
            if (!ToMeter.ContainsKey(from) || !ToMeter.ContainsKey(to))
                throw new ArgumentException($"Invalid length unit: {from} or {to}");

            // Convert to meters first, then to target unit
            double inMeters = value * ToMeter[from];
            double result = inMeters / ToMeter[to];
            
            return Math.Round(result, 4);
        }

        public static double GetConversionRate(string from, string to)
        {
            if (!ToMeter.ContainsKey(from) || !ToMeter.ContainsKey(to))
                throw new ArgumentException($"Invalid length unit: {from} or {to}");

            double inMeters = 1 * ToMeter[from];
            return Math.Round(inMeters / ToMeter[to], 4);
        }

        public static List<string> GetUnits()
        {
            return new List<string>(ToMeter.Keys);
        }
    }
}