using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementBusinessLayer.Validators;

public class ValidMeasurementTypeAttribute : ValidationAttribute
{
    private readonly string[] _validTypes = new[] { "LengthUnit", "WeightUnit", "VolumeUnit", "TemperatureUnit" };
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        
        var measurementType = value.ToString();
        
        if (string.IsNullOrWhiteSpace(measurementType))
        {
            return ValidationResult.Success;
        }
        
        if (!_validTypes.Contains(measurementType))
        {
            return new ValidationResult($"Measurement type must be one of: {string.Join(", ", _validTypes)}");
        }
        
        return ValidationResult.Success;
    }
}