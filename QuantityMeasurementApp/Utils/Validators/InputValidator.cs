using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Utils.Validators;

public static class InputValidator
{
    public static bool IsValidNumber(string? input, out double value)
    {
        value = 0;
        return !string.IsNullOrWhiteSpace(input) && 
               double.TryParse(input, out value) && 
               !double.IsNaN(value) && 
               !double.IsInfinity(value);
    }

    public static void ValidatePositiveNumber(double value, bool allowZero = true)
    {
        if (allowZero && value < 0)
            throw new InvalidValueException("Value cannot be negative");
        
        if (!allowZero && value <= 0)
            throw new InvalidValueException("Value must be positive");
    }

    public static void ValidateNotNull<T>(T? obj, string paramName) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
    }

    public static void ValidateRange(double value, double min, double max, string paramName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}");
    }

    public static bool IsValidWeightUnit(int choice)
    {
        return choice >= 1 && choice <= 3;
    }

    public static bool IsValidLengthUnit(int choice)
    {
        return choice >= 1 && choice <= 4;
    }

    public static bool IsValidVolumeUnit(int choice)
    {
        return choice >= 1 && choice <= 3;
    }

    public static bool IsValidTemperatureUnit(int choice)
    {
        return choice >= 1 && choice <= 3;
    }

    public static bool IsValidMenuChoice(int choice, int min, int max)
    {
        return choice >= min && choice <= max;
    }

    public static void ValidateFiniteNumber(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new InvalidValueException("Value must be a finite number");
    }

    public static void ValidateNonZero(double value)
    {
        if (Math.Abs(value) < 1e-10)
            throw new DivisionByZeroException("Value cannot be zero");
    }
}