using System;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Core.Domain.Quantities
{
    public class Quantity
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        public Quantity(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new InvalidValueException("Invalid value");

            Value = value;
            Unit = unit;
        }

        public Quantity ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = Unit.ConvertToBaseUnit(Value);

            double result = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity(Math.Round(result, 2), targetUnit);
        }

        public Quantity Add(Quantity other, LengthUnit targetUnit)
        {
            double base1 = Unit.ConvertToBaseUnit(Value);
            double base2 = other.Unit.ConvertToBaseUnit(other.Value);

            double sum = base1 + base2;

            double result = targetUnit.ConvertFromBaseUnit(sum);

            return new Quantity(Math.Round(result, 2), targetUnit);
        }

        public override bool Equals(object obj)
        {
            Quantity other = obj as Quantity;

            if (other == null)
                return false;

            double base1 = Unit.ConvertToBaseUnit(Value);
            double base2 = other.Unit.ConvertToBaseUnit(other.Value);

            return Math.Abs(base1 - base2) < 0.0001;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}