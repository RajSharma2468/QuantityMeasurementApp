using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Utils.Validators;

public static class MeasurementValidator
{
    /// <summary>
    /// Validates a measurement value
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <exception cref="InvalidValueException">Thrown when value is invalid</exception>
    public static void ValidateMeasurementValue(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new InvalidValueException("Measurement value must be a finite number");
    }

    /// <summary>
    /// Validates a measurement unit
    /// </summary>
    /// <typeparam name="T">The unit type implementing IMeasurable</typeparam>
    /// <param name="unit">The unit to validate</param>
    /// <exception cref="ArgumentNullException">Thrown when unit is null</exception>
    public static void ValidateMeasurementUnit<T>(T unit) where T : IMeasurable
    {
        if (unit == null)
            throw new ArgumentNullException(nameof(unit), "Measurement unit cannot be null");
    }

    /// <summary>
    /// Validates a complete measurement
    /// </summary>
    /// <typeparam name="T">The unit type implementing IMeasurable</typeparam>
    /// <param name="measurement">The measurement to validate</param>
    /// <exception cref="ArgumentNullException">Thrown when measurement is null</exception>
    public static void ValidateMeasurement<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        if (measurement == null)
            throw new ArgumentNullException(nameof(measurement));
        
        ValidateMeasurementValue(measurement.Value);
        ValidateMeasurementUnit(measurement.Unit);
    }

    /// <summary>
    /// Validates that two measurements are of the same unit type
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="first">First measurement</param>
    /// <param name="second">Second measurement</param>
    /// <exception cref="InvalidOperationException">Thrown when unit types don't match</exception>
    public static void ValidateSameUnitType<T>(GenericQuantity<T> first, GenericQuantity<T> second) where T : IMeasurable
    {
        if (first.Unit.GetType() != second.Unit.GetType())
            throw new InvalidOperationException("Cannot operate on different measurement types");
    }

    /// <summary>
    /// Validates conversion operation
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="source">Source measurement</param>
    /// <param name="targetUnit">Target unit</param>
    /// <exception cref="ArgumentNullException">Thrown when parameters are null</exception>
    public static void ValidateConversion<T>(GenericQuantity<T> source, T targetUnit) where T : IMeasurable
    {
        ValidateMeasurement(source);
        ValidateMeasurementUnit(targetUnit);
    }

    /// <summary>
    /// Validates addition operation
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="first">First measurement</param>
    /// <param name="second">Second measurement</param>
    /// <exception cref="ArgumentNullException">Thrown when parameters are null</exception>
    public static void ValidateAddition<T>(GenericQuantity<T> first, GenericQuantity<T> second) where T : IMeasurable
    {
        ValidateMeasurement(first);
        ValidateMeasurement(second);
    }

    /// <summary>
    /// Validates addition with target unit operation
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="first">First measurement</param>
    /// <param name="second">Second measurement</param>
    /// <param name="targetUnit">Target unit</param>
    /// <exception cref="ArgumentNullException">Thrown when parameters are null</exception>
    public static void ValidateAdditionWithTarget<T>(GenericQuantity<T> first, GenericQuantity<T> second, T targetUnit) where T : IMeasurable
    {
        ValidateMeasurement(first);
        ValidateMeasurement(second);
        ValidateMeasurementUnit(targetUnit);
    }

    /// <summary>
    /// Validates comparison operation
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="first">First measurement</param>
    /// <param name="second">Second measurement</param>
    /// <exception cref="ArgumentNullException">Thrown when parameters are null</exception>
    public static void ValidateComparison<T>(GenericQuantity<T> first, GenericQuantity<T> second) where T : IMeasurable
    {
        ValidateMeasurement(first);
        ValidateMeasurement(second);
    }

    /// <summary>
    /// Checks if a measurement is zero
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="measurement">The measurement to check</param>
    /// <returns>True if value is zero, false otherwise</returns>
    public static bool IsZero<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        return Math.Abs(measurement.Value) < 1e-10;
    }

    /// <summary>
    /// Checks if a measurement is positive
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="measurement">The measurement to check</param>
    /// <returns>True if value is positive, false otherwise</returns>
    public static bool IsPositive<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        return measurement.Value > 1e-10;
    }

    /// <summary>
    /// Checks if a measurement is negative
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="measurement">The measurement to check</param>
    /// <returns>True if value is negative, false otherwise</returns>
    public static bool IsNegative<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        return measurement.Value < -1e-10;
    }
}