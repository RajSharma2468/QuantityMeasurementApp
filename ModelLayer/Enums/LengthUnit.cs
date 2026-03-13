using ModelLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ModelLayer.Enums
{
    public enum LengthUnitType
    {
        FEET,
        INCH,
        YARD,
        CENTIMETER,
        METER
    }

    public class LengthUnit : IMeasurable
    {
        private readonly LengthUnitType _unit;
        private static readonly Dictionary<LengthUnitType, double> _conversionFactors;

        static LengthUnit()
        {
            _conversionFactors = new Dictionary<LengthUnitType, double>
            {
                { LengthUnitType.FEET, 1.0 },        // Base unit
                { LengthUnitType.INCH, 12.0 },        // 1 foot = 12 inches
                { LengthUnitType.YARD, 1.0/3.0 },     // 1 foot = 1/3 yard
                { LengthUnitType.CENTIMETER, 30.48 }, // 1 foot = 30.48 cm
                { LengthUnitType.METER, 0.3048 }      // 1 foot = 0.3048 meters
            };
        }

        public LengthUnit(LengthUnitType unit)
        {
            _unit = unit;
        }

        public double ToBaseUnit(double value)
        {
            return value * _conversionFactors[_unit];
        }

        public double FromBaseUnit(double value)
        {
            return value / _conversionFactors[_unit];
        }

        public string GetUnitName()
        {
            return _unit.ToString();
        }

        public string GetMeasurementType()
        {
            return "Length";
        }

        public IMeasurable GetUnitFromName(string unitName)
        {
            if (Enum.TryParse<LengthUnitType>(unitName, true, out var unitType))
            {
                return new LengthUnit(unitType);
            }
            throw new ArgumentException($"Invalid length unit: {unitName}");
        }

        public override bool Equals(object obj)
        {
            return obj is LengthUnit unit && _unit == unit._unit;
        }

        public override int GetHashCode()
        {
            return _unit.GetHashCode();
        }
    }
}