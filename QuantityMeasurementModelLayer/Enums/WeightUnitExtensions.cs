using System;

namespace QuantityMeasurementModelLayer.Enums
{
    public static class WeightUnitExtensions
    {
        public static double ToGrams(this WeightUnit unit, double value)
        {
            if (unit == WeightUnit.Milligram)
            {
                return value * 0.001;
            }
            else if (unit == WeightUnit.Gram)
            {
                return value;
            }
            else if (unit == WeightUnit.Kilogram)
            {
                return value * 1000.0;
            }
            else if (unit == WeightUnit.Ounce)
            {
                return value * 28.3495;
            }
            else if (unit == WeightUnit.Pound)
            {
                return value * 453.592;
            }
            else if (unit == WeightUnit.Ton)
            {
                return value * 907185.0;
            }
            else
            {
                throw new ArgumentException("Unknown weight unit: " + unit);
            }
        }
        
        public static double FromGrams(this WeightUnit unit, double grams)
        {
            if (unit == WeightUnit.Milligram)
            {
                return grams / 0.001;
            }
            else if (unit == WeightUnit.Gram)
            {
                return grams;
            }
            else if (unit == WeightUnit.Kilogram)
            {
                return grams / 1000.0;
            }
            else if (unit == WeightUnit.Ounce)
            {
                return grams / 28.3495;
            }
            else if (unit == WeightUnit.Pound)
            {
                return grams / 453.592;
            }
            else if (unit == WeightUnit.Ton)
            {
                return grams / 907185.0;
            }
            else
            {
                throw new ArgumentException("Unknown weight unit: " + unit);
            }
        }
        
        public static string GetSymbol(this WeightUnit unit)
        {
            if (unit == WeightUnit.Milligram)
            {
                return "mg";
            }
            else if (unit == WeightUnit.Gram)
            {
                return "g";
            }
            else if (unit == WeightUnit.Kilogram)
            {
                return "kg";
            }
            else if (unit == WeightUnit.Ounce)
            {
                return "oz";
            }
            else if (unit == WeightUnit.Pound)
            {
                return "lb";
            }
            else if (unit == WeightUnit.Ton)
            {
                return "t";
            }
            else
            {
                return unit.ToString();
            }
        }
    }
}