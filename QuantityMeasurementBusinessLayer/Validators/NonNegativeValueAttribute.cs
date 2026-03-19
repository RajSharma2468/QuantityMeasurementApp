using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementBusinessLayer.Validators;

public class NonNegativeValueAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }
        
        if (value is double doubleValue && doubleValue < 0)
        {
            return new ValidationResult("Value must be non-negative");
        }
        
        if (value is int intValue && intValue < 0)
        {
            return new ValidationResult("Value must be non-negative");
        }
        
        if (value is decimal decimalValue && decimalValue < 0)
        {
            return new ValidationResult("Value must be non-negative");
        }
        
        return ValidationResult.Success;
    }
}