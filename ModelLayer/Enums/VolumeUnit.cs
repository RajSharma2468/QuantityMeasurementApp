using ModelLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ModelLayer.Enums
{
    public enum VolumeUnitType
    {
        LITER,
        MILLILITER,
        GALLON
    }

    public class VolumeUnit : IMeasurable
    {
        private readonly VolumeUnitType _unit;
        private static readonly Dictionary<VolumeUnitType, double> _conversionFactors;

        static VolumeUnit()
        {
            _conversionFactors = new Dictionary<VolumeUnitType, double>
            {
                { VolumeUnitType.LITER, 1.0 },        // Base unit
                { VolumeUnitType.MILLILITER, 1000.0 }, // 1 liter = 1000 milliliters
                { VolumeUnitType.GALLON, 0.264172 }    // 1 liter = 0.264172 gallons
            };
        }

        public VolumeUnit(VolumeUnitType unit)
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
            return "Volume";
        }

        public IMeasurable GetUnitFromName(string unitName)
        {
            if (Enum.TryParse<VolumeUnitType>(unitName, true, out var unitType))
            {
                return new VolumeUnit(unitType);
            }
            throw new ArgumentException($"Invalid volume unit: {unitName}");
        }

        public override bool Equals(object obj)
        {
            return obj is VolumeUnit unit && _unit == unit._unit;
        }

        public override int GetHashCode()
        {
            return _unit.GetHashCode();
        }
    }
}