using ModelLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ModelLayer.Enums
{
    public enum WeightUnitType
    {
        GRAM,
        KILOGRAM,
        POUND,
        OUNCE
    }

    public class WeightUnit : IMeasurable
    {
        private readonly WeightUnitType _unit;
        private static readonly Dictionary<WeightUnitType, double> _conversionFactors;

        static WeightUnit()
        {
            _conversionFactors = new Dictionary<WeightUnitType, double>
            {
                { WeightUnitType.GRAM, 1.0 },         // Base unit
                { WeightUnitType.KILOGRAM, 0.001 },    // 1 gram = 0.001 kilogram
                { WeightUnitType.POUND, 0.00220462 },  // 1 gram = 0.00220462 pounds
                { WeightUnitType.OUNCE, 0.035274 }     // 1 gram = 0.035274 ounces
            };
        }

        public WeightUnit(WeightUnitType unit)
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
            return "Weight";
        }

        public IMeasurable GetUnitFromName(string unitName)
        {
            if (Enum.TryParse<WeightUnitType>(unitName, true, out var unitType))
            {
                return new WeightUnit(unitType);
            }
            throw new ArgumentException($"Invalid weight unit: {unitName}");
        }

        public override bool Equals(object obj)
        {
            return obj is WeightUnit unit && _unit == unit._unit;
        }

        public override int GetHashCode()
        {
            return _unit.GetHashCode();
        }
    }
}