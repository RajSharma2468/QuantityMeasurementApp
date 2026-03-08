using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Utils.Validators;

public static class UnitValidator
{
    /// <summary>
    /// Validates if a unit is a valid LengthUnit
    /// </summary>
    /// <param name="unit">The unit to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidLengthUnit(LengthUnit? unit)
    {
        if (unit == null) return false;
        
        return unit.Equals(LengthUnit.Inch) ||
               unit.Equals(LengthUnit.Feet) ||
               unit.Equals(LengthUnit.Yard) ||
               unit.Equals(LengthUnit.Centimeter);
    }

    /// <summary>
    /// Validates if a unit is a valid WeightUnit
    /// </summary>
    /// <param name="unit">The unit to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidWeightUnit(WeightUnit? unit)
    {
        if (unit == null) return false;
        
        return unit.Equals(WeightUnit.Kilogram) ||
               unit.Equals(WeightUnit.Gram) ||
               unit.Equals(WeightUnit.Pound);
    }

    /// <summary>
    /// Validates if a unit implements IMeasurable
    /// </summary>
    /// <param name="unit">The unit to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidMeasurableUnit(IMeasurable? unit)
    {
        return unit != null;
    }

    /// <summary>
    /// Gets the base unit for a given unit type
    /// </summary>
    /// <param name="unit">The unit</param>
    /// <returns>The base unit</returns>
    public static IMeasurable GetBaseUnit(IMeasurable unit)
    {
        return unit switch
        {
            LengthUnit => LengthUnit.Feet,
            WeightUnit => WeightUnit.Kilogram,
            _ => unit
        };
    }

    /// <summary>
    /// Checks if two units are of the same category
    /// </summary>
    /// <param name="unit1">First unit</param>
    /// <param name="unit2">Second unit</param>
    /// <returns>True if same category, false otherwise</returns>
    public static bool AreSameCategory(IMeasurable unit1, IMeasurable unit2)
    {
        if (unit1 == null || unit2 == null) return false;
        
        return unit1.GetType() == unit2.GetType();
    }

    /// <summary>
    /// Validates that two units are of the same category
    /// </summary>
    /// <param name="unit1">First unit</param>
    /// <param name="unit2">Second unit</param>
    /// <exception cref="InvalidOperationException">Thrown when units are from different categories</exception>
    public static void ValidateSameCategory(IMeasurable unit1, IMeasurable unit2)
    {
        if (!AreSameCategory(unit1, unit2))
            throw new InvalidOperationException($"Cannot operate on different measurement categories: {unit1.GetType().Name} and {unit2.GetType().Name}");
    }

    /// <summary>
    /// Gets all available length units
    /// </summary>
    /// <returns>List of length units</returns>
    public static List<LengthUnit> GetAllLengthUnits()
    {
        return new List<LengthUnit>
        {
            LengthUnit.Inch,
            LengthUnit.Feet,
            LengthUnit.Yard,
            LengthUnit.Centimeter
        };
    }

    /// <summary>
    /// Gets all available weight units
    /// </summary>
    /// <returns>List of weight units</returns>
    public static List<WeightUnit> GetAllWeightUnits()
    {
        return new List<WeightUnit>
        {
            WeightUnit.Kilogram,
            WeightUnit.Gram,
            WeightUnit.Pound
        };
    }

    /// <summary>
    /// Gets the unit symbol for display
    /// </summary>
    /// <param name="unit">The unit</param>
    /// <returns>Display symbol</returns>
    public static string GetUnitDisplaySymbol(IMeasurable unit)
    {
        return unit?.GetUnitSymbol() ?? "?";
    }

    /// <summary>
    /// Gets the unit name for display
    /// </summary>
    /// <param name="unit">The unit</param>
    /// <returns>Display name</returns>
    public static string GetUnitDisplayName(IMeasurable unit)
    {
        return unit?.GetUnitName() ?? "Unknown";
    }

    /// <summary>
    /// Parses a string to a LengthUnit
    /// </summary>
    /// <param name="unitString">The unit string</param>
    /// <returns>Parsed LengthUnit or null if invalid</returns>
    public static LengthUnit? ParseLengthUnit(string? unitString)
    {
        if (string.IsNullOrWhiteSpace(unitString)) return null;

        return unitString.Trim().ToLower() switch
        {
            "inch" or "in" => LengthUnit.Inch,
            "feet" or "ft" => LengthUnit.Feet,
            "yard" or "yd" => LengthUnit.Yard,
            "centimeter" or "cm" => LengthUnit.Centimeter,
            _ => null
        };
    }

    /// <summary>
    /// Parses a string to a WeightUnit
    /// </summary>
    /// <param name="unitString">The unit string</param>
    /// <returns>Parsed WeightUnit or null if invalid</returns>
    public static WeightUnit? ParseWeightUnit(string? unitString)
    {
        if (string.IsNullOrWhiteSpace(unitString)) return null;

        return unitString.Trim().ToLower() switch
        {
            "kilogram" or "kg" => WeightUnit.Kilogram,
            "gram" or "g" => WeightUnit.Gram,
            "pound" or "lb" => WeightUnit.Pound,
            _ => null
        };
    }
}