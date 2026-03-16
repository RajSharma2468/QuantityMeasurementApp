using System;

namespace QuantityMeasurementModelLayer.Enums
{
    public static class VolumeUnitExtensions
    {
        public static double ToMilliliters(this VolumeUnit unit, double value)
        {
            if (unit == VolumeUnit.Milliliter)
            {
                return value;
            }
            else if (unit == VolumeUnit.Liter)
            {
                return value * 1000.0;
            }
            else if (unit == VolumeUnit.Gallon)
            {
                return value * 3785.41;
            }
            else if (unit == VolumeUnit.Quart)
            {
                return value * 946.353;
            }
            else if (unit == VolumeUnit.Pint)
            {
                return value * 473.176;
            }
            else if (unit == VolumeUnit.Cup)
            {
                return value * 236.588;
            }
            else if (unit == VolumeUnit.Tablespoon)
            {
                return value * 14.7868;
            }
            else if (unit == VolumeUnit.Teaspoon)
            {
                return value * 4.92892;
            }
            else
            {
                throw new ArgumentException("Unknown volume unit: " + unit);
            }
        }
        
        public static double FromMilliliters(this VolumeUnit unit, double milliliters)
        {
            if (unit == VolumeUnit.Milliliter)
            {
                return milliliters;
            }
            else if (unit == VolumeUnit.Liter)
            {
                return milliliters / 1000.0;
            }
            else if (unit == VolumeUnit.Gallon)
            {
                return milliliters / 3785.41;
            }
            else if (unit == VolumeUnit.Quart)
            {
                return milliliters / 946.353;
            }
            else if (unit == VolumeUnit.Pint)
            {
                return milliliters / 473.176;
            }
            else if (unit == VolumeUnit.Cup)
            {
                return milliliters / 236.588;
            }
            else if (unit == VolumeUnit.Tablespoon)
            {
                return milliliters / 14.7868;
            }
            else if (unit == VolumeUnit.Teaspoon)
            {
                return milliliters / 4.92892;
            }
            else
            {
                throw new ArgumentException("Unknown volume unit: " + unit);
            }
        }
        
        public static string GetSymbol(this VolumeUnit unit)
        {
            if (unit == VolumeUnit.Milliliter)
            {
                return "ml";
            }
            else if (unit == VolumeUnit.Liter)
            {
                return "L";
            }
            else if (unit == VolumeUnit.Gallon)
            {
                return "gal";
            }
            else if (unit == VolumeUnit.Quart)
            {
                return "qt";
            }
            else if (unit == VolumeUnit.Pint)
            {
                return "pt";
            }
            else if (unit == VolumeUnit.Cup)
            {
                return "cup";
            }
            else if (unit == VolumeUnit.Tablespoon)
            {
                return "tbsp";
            }
            else if (unit == VolumeUnit.Teaspoon)
            {
                return "tsp";
            }
            else
            {
                return unit.ToString();
            }
        }
    }
}