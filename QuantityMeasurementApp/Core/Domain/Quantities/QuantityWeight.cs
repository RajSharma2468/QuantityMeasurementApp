using System;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Core.Domain.Quantities;

public class QuantityWeight : IEquatable<QuantityWeight>
{
    public double Value { get; }
    public WeightUnit Unit { get; }

    public QuantityWeight(double value, WeightUnit unit)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentException("Value must be a finite number", nameof(value));
        
        if (!Enum.IsDefined(typeof(WeightUnit), unit))
            throw new ArgumentException($"Invalid weight unit: {unit}");

        Value = value;
        Unit = unit;
    }

    public double ConvertToBaseUnit() => Unit.ConvertToBaseUnit(Value);

    public QuantityWeight ConvertTo(WeightUnit targetUnit)
    {
        if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
            throw new ArgumentException($"Invalid target unit: {targetUnit}");

        var baseValue = ConvertToBaseUnit();
        var convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);
        return new QuantityWeight(convertedValue, targetUnit);
    }

    public QuantityWeight Add(QuantityWeight other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = Unit.ConvertFromBaseUnit(baseSum);
        return new QuantityWeight(resultValue, Unit);
    }

    public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));
        if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
            throw new ArgumentException($"Invalid target unit: {targetUnit}");

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = targetUnit.ConvertFromBaseUnit(baseSum);
        return new QuantityWeight(resultValue, targetUnit);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as QuantityWeight);
    }

    public bool Equals(QuantityWeight other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;

        const double epsilon = 1e-10;
        var thisBaseValue = ConvertToBaseUnit();
        var otherBaseValue = other.ConvertToBaseUnit();
        return Math.Abs(thisBaseValue - otherBaseValue) < epsilon;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Math.Round(ConvertToBaseUnit(), 10), Unit);
    }

    public override string ToString()
    {
        return $"{Value:F2} {Unit.GetUnitSymbol()}";
    }
}