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

    // Separate validation methods for different scenarios
    private void ValidateBasicOperands(GenericQuantity<T> other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));
        
        if (Unit.GetType() != other.Unit.GetType())
            throw new InvalidOperationException($"Cannot operate on different measurement categories: {Unit.GetType().Name} and {other.Unit.GetType().Name}");
        
        if (double.IsNaN(other.Value) || double.IsInfinity(other.Value))
            throw new InvalidValueException("Operand value must be a finite number");
    }

    private void ValidateTargetUnit(T targetUnit)
    {
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));
    }

    private enum ArithmeticOperation
    {
        ADD,
        SUBTRACT,
        DIVIDE
    }

    private double PerformBaseArithmetic(GenericQuantity<T> other, ArithmeticOperation operation)
    {
        ValidateBasicOperands(other);
        
        double thisBase = ConvertToBaseUnit();
        double otherBase = other.ConvertToBaseUnit();
        
        return operation switch
        {
            ArithmeticOperation.ADD => thisBase + otherBase,
            ArithmeticOperation.SUBTRACT => thisBase - otherBase,
            ArithmeticOperation.DIVIDE when Math.Abs(otherBase) < Epsilon => 
                throw new DivisionByZeroException("Cannot divide by zero"),
            ArithmeticOperation.DIVIDE => thisBase / otherBase,
            _ => throw new InvalidOperationException("Unsupported arithmetic operation")
        };
    }

    public GenericQuantity<T> Add(GenericQuantity<T> other)
    {
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
        double resultValue = Unit.ConvertFromBaseUnit(baseResult);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), Unit);
    }

    public GenericQuantity<T> Add(GenericQuantity<T> other, T targetUnit)
    {
        ValidateBasicOperands(other);
        ValidateTargetUnit(targetUnit);
        
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
        double resultValue = targetUnit.ConvertFromBaseUnit(baseResult);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), targetUnit);
    }

    public GenericQuantity<T> Subtract(GenericQuantity<T> other)
    {
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
        double resultValue = Unit.ConvertFromBaseUnit(baseResult);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), Unit);
    }

    public GenericQuantity<T> Subtract(GenericQuantity<T> other, T targetUnit)
    {
        ValidateBasicOperands(other);
        ValidateTargetUnit(targetUnit);
        
        double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
        double resultValue = targetUnit.ConvertFromBaseUnit(baseResult);
        return new GenericQuantity<T>(Math.Round(resultValue, 4), targetUnit);
    }

    public double Divide(GenericQuantity<T> other)
    {
        return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as GenericQuantity<T>);
    }

    public bool Equals(GenericQuantity<T>? other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        double thisBaseValue = ConvertToBaseUnit();
        double otherBaseValue = other.ConvertToBaseUnit();
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