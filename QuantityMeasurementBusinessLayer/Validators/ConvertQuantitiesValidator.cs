using FluentValidation;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Validators;

public class ConvertQuantitiesValidator : BaseValidator<QuantityInputDTO>
{
    public ConvertQuantitiesValidator()
    {
        RuleFor(x => x.ThisQuantity)
            .NotNull().WithMessage("Source quantity is required");
        
        RuleFor(x => x.ThatQuantity)
            .NotNull().WithMessage("Target unit is required");
        
        RuleFor(x => x)
            .Must(HaveSameMeasurementType)
            .WithMessage("Cannot convert between different measurement types")
            .When(x => x.ThisQuantity != null && x.ThatQuantity != null);
        
        RuleFor(x => x.ThatQuantity.Value)
            .Equal(0).WithMessage("Target value should be 0 for conversion operations")
            .When(x => x.ThatQuantity != null);
        
        RuleFor(x => x.ThisQuantity.Unit)
            .Must((input, unit) => CanConvert(unit, input.ThatQuantity?.Unit))
            .WithMessage("Cannot convert between these units")
            .When(x => x.ThisQuantity != null && x.ThatQuantity != null);
    }
    
    private bool HaveSameMeasurementType(QuantityInputDTO input)
    {
        return input.ThisQuantity.MeasurementType == input.ThatQuantity.MeasurementType;
    }
    
    private bool CanConvert(string fromUnit, string? toUnit)  // Fix: make toUnit nullable
    {
        if (string.IsNullOrWhiteSpace(fromUnit) || string.IsNullOrWhiteSpace(toUnit))
            return false;
            
        // All units within same measurement type can be converted
        return true;
    }
}