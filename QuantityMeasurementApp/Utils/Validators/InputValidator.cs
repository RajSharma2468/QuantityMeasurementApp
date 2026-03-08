using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Utils.Validators;

public static class InputValidator
{
    /// <summary>
    /// Validates if the input string is a valid number
    /// </summary>
    /// <param name="input">The input string to validate</param>
    /// <param name="value">The parsed double value</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidNumber(string? input, out double value)
    {
        value = 0;
        return !string.IsNullOrWhiteSpace(input) && 
               double.TryParse(input, out value) && 
               !double.IsNaN(value) && 
               !double.IsInfinity(value);
    }

    /// <summary>
    /// Validates if the value is a positive number
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <param name="allowZero">Whether zero is allowed</param>
    /// <exception cref="InvalidValueException">Thrown when validation fails</exception>
    public static void ValidatePositiveNumber(double value, bool allowZero = true)
    {
        if (allowZero && value < 0)
            throw new InvalidValueException("Value cannot be negative");
        
        if (!allowZero && value <= 0)
            throw new InvalidValueException("Value must be positive");
    }

    /// <summary>
    /// Validates that an object is not null
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    /// <param name="obj">The object to validate</param>
    /// <param name="paramName">Parameter name for exception</param>
    /// <exception cref="ArgumentNullException">Thrown when object is null</exception>
    public static void ValidateNotNull<T>(T? obj, string paramName) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Validates if the value is within a specified range
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <param name="min">Minimum allowed value</param>
    /// <param name="max">Maximum allowed value</param>
    /// <param name="paramName">Parameter name for exception</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when value is out of range</exception>
    public static void ValidateRange(double value, double min, double max, string paramName)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}");
    }

    /// <summary>
    /// Validates if the string is not null or whitespace
    /// </summary>
    /// <param name="input">The string to validate</param>
    /// <param name="paramName">Parameter name for exception</param>
    /// <exception cref="ArgumentException">Thrown when string is null or whitespace</exception>
    public static void ValidateString(string? input, string paramName)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException($"{paramName} cannot be null or whitespace", paramName);
    }

    /// <summary>
    /// Validates weight unit choice
    /// </summary>
    /// <param name="choice">The selected choice</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidWeightUnit(int choice)
    {
        return choice >= 1 && choice <= 3;
    }

    /// <summary>
    /// Validates length unit choice
    /// </summary>
    /// <param name="choice">The selected choice</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidLengthUnit(int choice)
    {
        return choice >= 1 && choice <= 4;
    }

    /// <summary>
    /// Validates menu choice
    /// </summary>
    /// <param name="choice">The selected choice</param>
    /// <param name="min">Minimum allowed value</param>
    /// <param name="max">Maximum allowed value</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidMenuChoice(int choice, int min, int max)
    {
        return choice >= min && choice <= max;
    }

    /// <summary>
    /// Validates if two quantities are of the same type
    /// </summary>
    /// <typeparam name="T">The type to check</typeparam>
    /// <param name="obj1">First object</param>
    /// <param name="obj2">Second object</param>
    /// <returns>True if same type, false otherwise</returns>
    public static bool AreSameType<T>(T obj1, T obj2)
    {
        if (obj1 == null || obj2 == null)
            return false;
        
        return obj1.GetType() == obj2.GetType();
    }

    /// <summary>
    /// Validates if the value is a finite number
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <exception cref="InvalidValueException">Thrown when value is not finite</exception>
    public static void ValidateFiniteNumber(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new InvalidValueException("Value must be a finite number");
    }

    /// <summary>
    /// Validates if the unit is valid (not null)
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="unit">The unit to validate</param>
    /// <exception cref="ArgumentNullException">Thrown when unit is null</exception>
    public static void ValidateUnit<T>(T unit) where T : class
    {
        if (unit == null)
            throw new ArgumentNullException(nameof(unit), "Unit cannot be null");
    }

    /// <summary>
    /// Validates conversion parameters
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="source">Source quantity</param>
    /// <param name="targetUnit">Target unit</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
    public static void ValidateConversionParams<T>(object source, T targetUnit) where T : class
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));
    }

    /// <summary>
    /// Validates addition parameters
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="first">First quantity</param>
    /// <param name="second">Second quantity</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
    public static void ValidateAdditionParams<T>(object first, object second)
    {
        if (first == null)
            throw new ArgumentNullException(nameof(first));
        
        if (second == null)
            throw new ArgumentNullException(nameof(second));
    }

    /// <summary>
    /// Validates addition with target parameters
    /// </summary>
    /// <typeparam name="T">The unit type</typeparam>
    /// <param name="first">First quantity</param>
    /// <param name="second">Second quantity</param>
    /// <param name="targetUnit">Target unit</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
    public static void ValidateAdditionWithTargetParams<T>(object first, object second, T targetUnit) where T : class
    {
        if (first == null)
            throw new ArgumentNullException(nameof(first));
        
        if (second == null)
            throw new ArgumentNullException(nameof(second));
        
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));
    }

    /// <summary>
    /// Validates comparison parameters
    /// </summary>
    /// <param name="first">First quantity</param>
    /// <param name="second">Second quantity</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null</exception>
    public static void ValidateComparisonParams(object first, object second)
    {
        if (first == null)
            throw new ArgumentNullException(nameof(first));
        
        if (second == null)
            throw new ArgumentNullException(nameof(second));
    }

    /// <summary>
    /// Tries to parse a double value from string input
    /// </summary>
    /// <param name="input">The input string</param>
    /// <param name="value">The parsed value</param>
    /// <returns>True if parsing successful, false otherwise</returns>
    public static bool TryParseDouble(string? input, out double value)
    {
        value = 0;
        return !string.IsNullOrWhiteSpace(input) && double.TryParse(input, out value);
    }

    /// <summary>
    /// Tries to parse an integer value from string input
    /// </summary>
    /// <param name="input">The input string</param>
    /// <param name="value">The parsed value</param>
    /// <returns>True if parsing successful, false otherwise</returns>
    public static bool TryParseInt(string? input, out int value)
    {
        value = 0;
        return !string.IsNullOrWhiteSpace(input) && int.TryParse(input, out value);
    }

    /// <summary>
    /// Validates if the value is within tolerance of expected value
    /// </summary>
    /// <param name="expected">Expected value</param>
    /// <param name="actual">Actual value</param>
    /// <param name="tolerance">Tolerance for comparison</param>
    /// <returns>True if within tolerance, false otherwise</returns>
    public static bool IsWithinTolerance(double expected, double actual, double tolerance = 1e-10)
    {
        return Math.Abs(expected - actual) < tolerance;
    }
}