using FluentValidation;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Validators;

public class CompareQuantitiesValidator : BaseValidator<QuantityInputDTO>
{
    public CompareQuantitiesValidator()
    {
        RuleFor(x => x.ThisQuantity)
            .NotNull().WithMessage("First quantity is required");
        
        RuleFor(x => x.ThatQuantity)
            .NotNull().WithMessage("Second quantity is required");
        
        RuleFor(x => x)
            .Must(HaveSameMeasurementType)
            .WithMessage("Cannot compare quantities of different measurement types")
            .When(x => x.ThisQuantity != null && x.ThatQuantity != null);
        
        RuleFor(x => x)
            .Must(HaveValidUnitsForComparison)
            .WithMessage("Units are not compatible for comparison")
            .When(x => x.ThisQuantity != null && x.ThatQuantity != null);
    }
    
    private bool HaveSameMeasurementType(QuantityInputDTO input)
    {
        return input.ThisQuantity.MeasurementType == input.ThatQuantity.MeasurementType;
    }
    
    private bool HaveValidUnitsForComparison(QuantityInputDTO input)
    {
        // Both units should be valid for their respective measurement types
        var thisUnitValid = IsValidUnit(input.ThisQuantity.Unit, input.ThisQuantity.MeasurementType);
        var thatUnitValid = IsValidUnit(input.ThatQuantity.Unit, input.ThatQuantity.MeasurementType);
        
        return thisUnitValid && thatUnitValid;
    }
    
    private bool IsValidUnit(string unit, string measurementType)
    {
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