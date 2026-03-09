using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Utils.Validators;

public static class MeasurementValidator
{
    public static void ValidateMeasurementValue(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new InvalidValueException("Measurement value must be a finite number");
    }

    public static void ValidateMeasurementUnit<T>(T unit) where T : IMeasurable
    {
        if (unit == null)
            throw new ArgumentNullException(nameof(unit), "Measurement unit cannot be null");
    }

    public static void ValidateMeasurement<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        if (measurement == null)
            throw new ArgumentNullException(nameof(measurement));
        
        ValidateMeasurementValue(measurement.Value);
        ValidateMeasurementUnit(measurement.Unit);
    }

    public static bool IsZero<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        return Math.Abs(measurement.Value) < 1e-10;
    }

    public static bool IsPositive<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        return measurement.Value > 1e-10;
    }

    public static bool IsNegative<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        return measurement.Value < -1e-10;
    }
}