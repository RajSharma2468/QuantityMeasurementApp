using System.Text.RegularExpressions;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementConsoleApp.Helpers;

public static class ValidationHelper
{
    private static readonly HashSet<string> ValidMeasurementTypes = new()
    {
        "LengthUnit", "WeightUnit", "VolumeUnit", "TemperatureUnit"
    };

    private static readonly Dictionary<string, HashSet<string>> ValidUnits = new()
    {
        ["LengthUnit"] = new() { "Inch", "Feet", "Yard", "Centimeter", "Meter" },
        ["WeightUnit"] = new() { "Gram", "Kilogram", "Pound", "Ounce" },
        ["VolumeUnit"] = new() { "Milliliter", "Liter", "Gallon", "Cup" },
        ["TemperatureUnit"] = new() { "Celsius", "Fahrenheit", "Kelvin" }
    };

    public static bool IsValidMeasurementType(string measurementType)
    {
        return !string.IsNullOrWhiteSpace(measurementType) && 
               ValidMeasurementTypes.Contains(measurementType);
    }

    public static bool IsValidUnit(string unit, string measurementType)
    {
        if (string.IsNullOrWhiteSpace(unit) || string.IsNullOrWhiteSpace(measurementType))
            return false;

        return ValidUnits.ContainsKey(measurementType) && 
               ValidUnits[measurementType].Contains(unit);
    }

    public static bool IsValidDouble(string input, out double value, double min = 0, double max = double.MaxValue)
    {
        value = 0;
        return double.TryParse(input, out value) && value >= min && value <= max;
    }

    public static bool IsValidInteger(string input, out int value, int min = 0, int max = int.MaxValue)
    {
        value = 0;
        return int.TryParse(input, out value) && value >= min && value <= max;
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    public static string GetValidMeasurementType()
    {
        while (true)
        {
            var input = ConsoleHelper.GetUserInput("Enter measurement type (LengthUnit/WeightUnit/VolumeUnit/TemperatureUnit)");
            
            if (IsValidMeasurementType(input))
                return input;

            ConsoleHelper.PrintError("Invalid measurement type. Please choose from: LengthUnit, WeightUnit, VolumeUnit, TemperatureUnit");
        }
    }

    public static string GetValidUnit(string measurementType)
    {
        var validUnits = ValidUnits[measurementType];
        var unitsList = string.Join("/", validUnits);

        while (true)
        {
            var input = ConsoleHelper.GetUserInput($"Enter unit ({unitsList})");
            
            if (IsValidUnit(input, measurementType))
                return input;

            ConsoleHelper.PrintError($"Invalid unit. Please choose from: {unitsList}");
        }
    }

    public static double GetValidDouble(string prompt, double min = 0, double max = double.MaxValue)
    {
        while (true)
        {
            var input = ConsoleHelper.GetUserInput(prompt, false);
            
            if (IsValidDouble(input, out double value, min, max))
                return value;

            ConsoleHelper.PrintError($"Please enter a valid number between {min} and {max}");
        }
    }

    public static List<string> GetSuggestions(string input, string? measurementType = null)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new List<string>();

        var suggestions = new List<string>();

        if (measurementType == null)
        {
            suggestions.AddRange(ValidMeasurementTypes
                .Where(t => t.StartsWith(input, StringComparison.OrdinalIgnoreCase)));
        }
        else if (ValidUnits.ContainsKey(measurementType))
        {
            suggestions.AddRange(ValidUnits[measurementType]
                .Where(u => u.StartsWith(input, StringComparison.OrdinalIgnoreCase)));
        }

        return suggestions.Take(5).ToList();
    }

    public static bool IsValidOperation(string operation)
    {
        var validOperations = new[] { "compare", "convert", "add", "subtract", "multiply", "divide" };
        return !string.IsNullOrWhiteSpace(operation) && 
               validOperations.Contains(operation.ToLower());
    }

    public static bool AreUnitsCompatible(string unit1, string unit2, string measurementType)
    {
        if (!IsValidUnit(unit1, measurementType) || !IsValidUnit(unit2, measurementType))
            return false;

        return true; // All units within same measurement type are compatible
    }

    public static (bool IsValid, string ErrorMessage) ValidateQuantityInput(QuantityInputDTO input)
    {
        if (input == null)
            return (false, "Input cannot be null");

        if (input.ThisQuantity == null || input.ThatQuantity == null)
            return (false, "Both quantities are required");

        if (!IsValidMeasurementType(input.ThisQuantity.MeasurementType))
            return (false, $"Invalid measurement type: {input.ThisQuantity.MeasurementType}");

        if (!IsValidMeasurementType(input.ThatQuantity.MeasurementType))
            return (false, $"Invalid measurement type: {input.ThatQuantity.MeasurementType}");

        if (!IsValidUnit(input.ThisQuantity.Unit, input.ThisQuantity.MeasurementType))
            return (false, $"Invalid unit '{input.ThisQuantity.Unit}' for {input.ThisQuantity.MeasurementType}");

        if (!IsValidUnit(input.ThatQuantity.Unit, input.ThatQuantity.MeasurementType))
            return (false, $"Invalid unit '{input.ThatQuantity.Unit}' for {input.ThatQuantity.MeasurementType}");

        if (input.ThisQuantity.Value < 0)
            return (false, "Value cannot be negative");

        if (input.ThatQuantity.Value < 0)
            return (false, "Value cannot be negative");

        return (true, string.Empty);
    }
}