using FluentValidation;
using FluentValidation.Results;

namespace QuantityMeasurementBusinessLayer.Validators;

public abstract class BaseValidator<T> : AbstractValidator<T>
{
    protected BaseValidator()
    {
        // Common validation rules can go here
    }
    
    public new async Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = default)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance), "Validation object cannot be null");
        }
        
        return await base.ValidateAsync(instance, cancellation);
    }
    
    protected bool BeValidMeasurementType(string measurementType)
    {
        if (string.IsNullOrWhiteSpace(measurementType))
            return false;
            
        var validTypes = new[] { "LengthUnit", "WeightUnit", "VolumeUnit", "TemperatureUnit" };
        return validTypes.Contains(measurementType);
    }
    
    protected bool BeValidUnit(string unit, string measurementType)
    {
        if (string.IsNullOrWhiteSpace(unit))
            return false;
            
        return measurementType?.ToLower() switch
        {
            "lengthunit" => new[] { "Inch", "Feet", "Yard", "Centimeter", "Meter" }.Contains(unit),
            "weightunit" => new[] { "Gram", "Kilogram", "Pound", "Ounce" }.Contains(unit),
            "volumeunit" => new[] { "Milliliter", "Liter", "Gallon", "Cup" }.Contains(unit),
            "temperatureunit" => new[] { "Celsius", "Fahrenheit", "Kelvin" }.Contains(unit),
            _ => false
        };
    }
}