using System;

namespace QuantityMeasurementModelLayer.Enums
{
    public static class LengthUnitExtensions
    {
        public static double ToMeters(this LengthUnit unit, double value)
        {
            if (unit == LengthUnit.Inch)
            {
                return value * 0.0254;
            }
            else if (unit == LengthUnit.Foot)
            {
                return value * 0.3048;
            }
            else if (unit == LengthUnit.Yard)
            {
                return value * 0.9144;
            }
            else if (unit == LengthUnit.Mile)
            {
                return value * 1609.344;
            }
            else if (unit == LengthUnit.Centimeter)
            {
                return value * 0.01;
            }
            else if (unit == LengthUnit.Meter)
            {
                return value;
            }
            else if (unit == LengthUnit.Kilometer)
            {
                return value * 1000.0;
            }
            else
            {
                throw new ArgumentException("Unknown length unit: " + unit);
            }
        }
        
        public static double FromMeters(this LengthUnit unit, double meters)
        {
            if (unit == LengthUnit.Inch)
            {
                return meters / 0.0254;
            }
            else if (unit == LengthUnit.Foot)
            {
                return meters / 0.3048;
            }
            else if (unit == LengthUnit.Yard)
            {
                return meters / 0.9144;
            }
            else if (unit == LengthUnit.Mile)
            {
                return meters / 1609.344;
            }
            else if (unit == LengthUnit.Centimeter)
            {
                return meters / 0.01;
            }
            else if (unit == LengthUnit.Meter)
            {
                return meters;
            }
            else if (unit == LengthUnit.Kilometer)
            {
                return meters / 1000.0;
            }
            else
            {
                throw new ArgumentException("Unknown length unit: " + unit);
            }
        }
        
        public static string GetSymbol(this LengthUnit unit)
        {
            if (unit == LengthUnit.Inch)
            {
                return "in";
            }
            else if (unit == LengthUnit.Foot)
            {
                return "ft";
            }
            else if (unit == LengthUnit.Yard)
            {
                return "yd";
            }
            else if (unit == LengthUnit.Mile)
            {
                return "mi";
            }
            else if (unit == LengthUnit.Centimeter)
            {
                return "cm";
            }
            else if (unit == LengthUnit.Meter)
            {
                return "m";
            }
            else if (unit == LengthUnit.Kilometer)
            {
                return "km";
            }
            else
            {
                return unit.ToString();
            }
        }
    }
}