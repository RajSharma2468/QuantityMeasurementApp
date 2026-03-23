using System;
using System.Collections.Generic;

namespace QuantityMeasurementBusinessLayer.Converters
{
    public static class WeightConverter
    {
        // Base unit: Kilogram
        private static readonly Dictionary<string, double> ToKg = new Dictionary<string, double>
        {
            { "Kilogram", 1 },
            { "Gram", 0.001 },
            { "Pound", 0.453592 },
            { "Ounce", 0.0283495 }
        };

        public static double Convert(string from, string to, double value)
        {
            if (!ToKg.ContainsKey(from) || !ToKg.ContainsKey(to))
                throw new ArgumentException($"Invalid weight unit: {from} or {to}");

            double inKg = value * ToKg[from];
            double result = inKg / ToKg[to];
            
            return Math.Round(result, 4);
        }

        public static double GetConversionRate(string from, string to)
        {
            if (!ToKg.ContainsKey(from) || !ToKg.ContainsKey(to))
                throw new ArgumentException($"Invalid weight unit: {from} or {to}");

            double inKg = 1 * ToKg[from];
            return Math.Round(inKg / ToKg[to], 4);
        }

        public static List<string> GetUnits()
        {
            return new List<string>(ToKg.Keys);
        }
    }
}