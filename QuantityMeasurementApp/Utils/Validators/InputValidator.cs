using System;

namespace QuantityMeasurementApp.Core.Utils.Validators;

public static class InputValidator
{
    public static bool IsValidNumber(string input, out double value)
    {
        value = 0;
        return !string.IsNullOrWhiteSpace(input) && 
               double.TryParse(input, out value) && 
               !double.IsNaN(value) && 
               !double.IsInfinity(value);
    }

    public static bool IsValidWeightUnit(int choice)
    {
        return choice >= 1 && choice <= 3;
    }

    public static bool IsValidLengthUnit(int choice)
    {
        return choice >= 1 && choice <= 4;
    }

    public static bool IsPositiveNumber(double value, bool allowZero = true)
    {
        return allowZero ? value >= 0 : value > 0;
    }

    public static void ValidateNotNull<T>(T obj, string paramName) where T : class
    {
        if (obj is null)
            throw new ArgumentNullException(paramName);
    }
}