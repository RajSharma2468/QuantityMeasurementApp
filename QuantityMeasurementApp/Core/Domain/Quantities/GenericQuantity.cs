using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Core.Domain.Quantities;

public class GenericQuantity<T> : IEquatable<GenericQuantity<T>> where T : IMeasurable
{
    public double Value { get; }
    public T Unit { get; }
    private const double Epsilon = 1e-10;

    public GenericQuantity(double value, T unit)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new InvalidValueException("Value must be a finite number");
        
        if (unit == null)
            throw new ArgumentNullException(nameof(unit));

        Value = Math.Round(value, 4);
        Unit = unit;
    }

    public double ConvertToBaseUnit() => Unit.ConvertToBaseUnit(Value);

    public GenericQuantity<T> ConvertTo(T targetUnit)
    {
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));

        var baseValue = ConvertToBaseUnit();
        var convertedValue = targetUnit.ConvertFromBaseUnit(baseValue);
        return new GenericQuantity<T>(convertedValue, targetUnit);
    }

    public GenericQuantity<T> Add(GenericQuantity<T> other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = Unit.ConvertFromBaseUnit(baseSum);
        return new GenericQuantity<T>(resultValue, Unit);
    }

    public GenericQuantity<T> Add(GenericQuantity<T> other, T targetUnit)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = targetUnit.ConvertFromBaseUnit(baseSum);
        return new GenericQuantity<T>(resultValue, targetUnit);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as GenericQuantity<T>);
    }

    public bool Equals(GenericQuantity<T>? other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        var thisBaseValue = ConvertToBaseUnit();
        var otherBaseValue = other.ConvertToBaseUnit();
        return Math.Abs(thisBaseValue - otherBaseValue) < Epsilon;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Math.Round(ConvertToBaseUnit(), 10), Unit.GetType());
    }

    public override string ToString()
    {
        return $"{Value:F2} {Unit.GetUnitSymbol()}";
    }
}