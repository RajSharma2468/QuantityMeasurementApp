using ModelLayer.Interfaces;
using System;

namespace ModelLayer.Enums
{
    public enum TemperatureUnitType
    {
        CELSIUS,
        FAHRENHEIT,
        KELVIN
    }

    public class TemperatureUnit : IMeasurable
    {
        private readonly TemperatureUnitType _unit;

        public TemperatureUnit(TemperatureUnitType unit)
        {
            _unit = unit;
        }

        public double ToBaseUnit(double value)
        {
            switch (_unit)
            {
                case TemperatureUnitType.CELSIUS:
                    return value;  // Celsius as base unit
                case TemperatureUnitType.FAHRENHEIT:
                    return (value - 32) * 5 / 9;
                case TemperatureUnitType.KELVIN:
                    return value - 273.15;
                default:
                    throw new ArgumentException("Invalid temperature unit");
            }
        }

        public double FromBaseUnit(double value)
        {
            switch (_unit)
            {
                case TemperatureUnitType.CELSIUS:
                    return value;
                case TemperatureUnitType.FAHRENHEIT:
                    return (value * 9 / 5) + 32;
                case TemperatureUnitType.KELVIN:
                    return value + 273.15;
                default:
                    throw new ArgumentException("Invalid temperature unit");
            }
        }

        public string GetUnitName()
        {
            return _unit.ToString();
        }

        public string GetMeasurementType()
        {
            return "Temperature";
        }

        public IMeasurable GetUnitFromName(string unitName)
        {
            if (Enum.TryParse<TemperatureUnitType>(unitName, true, out var unitType))
            {
                return new TemperatureUnit(unitType);
            }
            throw new ArgumentException($"Invalid temperature unit: {unitName}");
        }

        public override bool Equals(object obj)
        {
            return obj is TemperatureUnit unit && _unit == unit._unit;
        }

        public override int GetHashCode()
        {
            return _unit.GetHashCode();
        }
    }
}