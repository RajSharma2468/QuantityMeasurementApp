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
        return new GenericQuantity<T>(Math.Round(convertedValue, 4), targetUnit);
    }

    public GenericQuantity<T> Add(GenericQuantity<T> other)
    {
        ValidateOperand(other, nameof(other));
        ValidateSameCategory(other);

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = Unit.ConvertFromBaseUnit(baseSum);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), Unit);
    }

    public GenericQuantity<T> Add(GenericQuantity<T> other, T targetUnit)
    {
        ValidateOperand(other, nameof(other));
        ValidateTargetUnit(targetUnit);
        ValidateSameCategory(other);

        var baseSum = ConvertToBaseUnit() + other.ConvertToBaseUnit();
        var resultValue = targetUnit.ConvertFromBaseUnit(baseSum);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), targetUnit);
    }

    public GenericQuantity<T> Subtract(GenericQuantity<T> other)
    {
        ValidateOperand(other, nameof(other));
        ValidateSameCategory(other);

        var baseDifference = ConvertToBaseUnit() - other.ConvertToBaseUnit();
        var resultValue = Unit.ConvertFromBaseUnit(baseDifference);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), Unit);
    }

    public GenericQuantity<T> Subtract(GenericQuantity<T> other, T targetUnit)
    {
        ValidateOperand(other, nameof(other));
        ValidateTargetUnit(targetUnit);
        ValidateSameCategory(other);

        var baseDifference = ConvertToBaseUnit() - other.ConvertToBaseUnit();
        var resultValue = targetUnit.ConvertFromBaseUnit(baseDifference);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), targetUnit);
    }

    public double Divide(GenericQuantity<T> other)
    {
        ValidateOperand(other, nameof(other));
        ValidateSameCategory(other);

        var otherBaseValue = other.ConvertToBaseUnit();
        
        if (Math.Abs(otherBaseValue) < Epsilon)
            throw new DivisionByZeroException("Cannot divide by zero quantity");

        var thisBaseValue = ConvertToBaseUnit();
        return thisBaseValue / otherBaseValue;
    }

    private void ValidateOperand(GenericQuantity<T> other, string paramName)
    {
        if (other == null)
            throw new ArgumentNullException(paramName);
    }

    private void ValidateTargetUnit(T targetUnit)
    {
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));
    }

    private void ValidateSameCategory(GenericQuantity<T> other)
    {
        if (Unit.GetType() != other.Unit.GetType())
            throw new InvalidOperationException($"Cannot operate on different measurement categories: {Unit.GetType().Name} and {other.Unit.GetType().Name}");
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