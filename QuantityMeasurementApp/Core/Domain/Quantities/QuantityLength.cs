using System;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Core.Domain.Quantities;

public class QuantityLength : IEquatable<QuantityLength>
{
    public double Value { get; }
    public LengthUnit Unit { get; }

    public QuantityLength(double value, LengthUnit unit)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentException("Value must be a finite number", nameof(value));
        
        if (!Enum.IsDefined(typeof(LengthUnit), unit))
            throw new ArgumentException($"Invalid length unit: {unit}");
        
        Value = value;
        Unit = unit;
    }

    public double ConvertToBaseUnit() => Unit.ConvertToBaseUnit(Value);

    public QuantityLength ConvertTo(LengthUnit targetUnit)
    {
        if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
            throw new ArgumentException($"Invalid target unit: {targetUnit}");

        var baseValue = ConvertToBaseUnit();
        var convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);
        return new QuantityLength(convertedValue, targetUnit);
    }

    public QuantityLength Add(QuantityLength other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = Unit.ConvertFromBaseUnit(baseSum);
        return new QuantityLength(resultValue, Unit);
    }

    public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));
        if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
            throw new ArgumentException($"Invalid target unit: {targetUnit}");

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = targetUnit.ConvertFromBaseUnit(baseSum);
        return new QuantityLength(resultValue, targetUnit);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as QuantityLength);
    }

    public bool Equals(QuantityLength other)
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