using ModelLayer.Interfaces;
using System;

namespace ModelLayer.Models
{
    public class Quantity<T> where T : IMeasurable
    {
        public double Value { get; }
        public T Unit { get; }

        public Quantity(double value, T unit)
        {
            Value = value;
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public double ConvertTo(T targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentNullException(nameof(targetUnit));

            if (!Unit.GetMeasurementType().Equals(targetUnit.GetMeasurementType()))
            {
                throw new InvalidOperationException(
                    $"Cannot convert between different measurement types: {Unit.GetMeasurementType()} and {targetUnit.GetMeasurementType()}");
            }

            double baseValue = Unit.ToBaseUnit(Value);
            return targetUnit.FromBaseUnit(baseValue);
        }

        public bool Equals(Quantity<T> other)
        {
            if (other == null) return false;

            double thisBaseValue = Unit.ToBaseUnit(Value);
            double otherBaseValue = other.Unit.ToBaseUnit(other.Value);

            return Math.Abs(thisBaseValue - otherBaseValue) < 0.0001;
        }

        public Quantity<T> Add(Quantity<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (!Unit.GetMeasurementType().Equals(other.Unit.GetMeasurementType()))
            {
                throw new InvalidOperationException(
                    $"Cannot add quantities of different measurement types: {Unit.GetMeasurementType()} and {other.Unit.GetMeasurementType()}");
            }

            double thisBaseValue = Unit.ToBaseUnit(Value);
            double otherBaseValue = other.Unit.ToBaseUnit(other.Value);
            double resultBaseValue = thisBaseValue + otherBaseValue;

            double resultValue = Unit.FromBaseUnit(resultBaseValue);
            return new Quantity<T>(resultValue, Unit);
        }

        public Quantity<T> Subtract(Quantity<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (!Unit.GetMeasurementType().Equals(other.Unit.GetMeasurementType()))
            {
                throw new InvalidOperationException(
                    $"Cannot subtract quantities of different measurement types: {Unit.GetMeasurementType()} and {other.Unit.GetMeasurementType()}");
            }

            double thisBaseValue = Unit.ToBaseUnit(Value);
            double otherBaseValue = other.Unit.ToBaseUnit(other.Value);
            double resultBaseValue = thisBaseValue - otherBaseValue;

            double resultValue = Unit.FromBaseUnit(resultBaseValue);
            return new Quantity<T>(resultValue, Unit);
        }

        public double Divide(Quantity<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (!Unit.GetMeasurementType().Equals(other.Unit.GetMeasurementType()))
            {
                throw new InvalidOperationException(
                    $"Cannot divide quantities of different measurement types: {Unit.GetMeasurementType()} and {other.Unit.GetMeasurementType()}");
            }

            if (Math.Abs(other.Value) < 0.0001)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }

            double thisBaseValue = Unit.ToBaseUnit(Value);
            double otherBaseValue = other.Unit.ToBaseUnit(other.Value);

            return thisBaseValue / otherBaseValue;
        }

        public override string ToString()
        {
            return $"{Value} {Unit.GetUnitName()}";
        }
    }
}