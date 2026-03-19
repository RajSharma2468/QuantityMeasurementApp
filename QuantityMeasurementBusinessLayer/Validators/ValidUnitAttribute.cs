using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementBusinessLayer.Validators;

public class ValidUnitAttribute : ValidationAttribute
{
    private readonly Dictionary<string, string[]> _validUnits = new()
    {
        ["LengthUnit"] = new[] { "Inch", "Feet", "Yard", "Centimeter", "Meter" },
        ["WeightUnit"] = new[] { "Gram", "Kilogram", "Pound", "Ounce" },
        ["VolumeUnit"] = new[] { "Milliliter", "Liter", "Gallon", "Cup" },
        ["TemperatureUnit"] = new[] { "Celsius", "Fahrenheit", "Kelvin" }
    };
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        
        var unit = value.ToString();
        
        if (string.IsNullOrWhiteSpace(unit))
        {
            return ValidationResult.Success;
        }
        
        // Try to get the measurement type from the containing object
        var instance = validationContext.ObjectInstance;
        var measurementTypeProperty = instance.GetType().GetProperty("MeasurementType");
        
        if (measurementTypeProperty != null)
        {
            var measurementType = measurementTypeProperty.GetValue(instance)?.ToString();
            
            if (!string.IsNullOrWhiteSpace(measurementType) && 
                _validUnits.ContainsKey(measurementType) && 
                !_validUnits[measurementType].Contains(unit))
            {
                return new ValidationResult($"Unit '{unit}' is not valid for {measurementType}");
            }
        }
        
        return ValidationResult.Success;
    }
}