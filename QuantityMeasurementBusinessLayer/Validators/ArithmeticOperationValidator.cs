using FluentValidation;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Validators;

public class ArithmeticOperationValidator : BaseValidator<QuantityInputDTO>
{
    public ArithmeticOperationValidator()  // Remove the string parameter
    {
        RuleFor(x => x.ThisQuantity)
            .NotNull().WithMessage("First quantity is required");
        
        RuleFor(x => x.ThatQuantity)
            .NotNull().WithMessage("Second quantity is required");
        
        RuleFor(x => x)
            .Must(HaveSameMeasurementType)
            .WithMessage("Cannot perform arithmetic between different measurement types")
            .When(x => x.ThisQuantity != null && x.ThatQuantity != null);
        
        // Don't validate division by zero here - it's handled in service
    }
    
    private bool HaveSameMeasurementType(QuantityInputDTO input)
    {
        return input.ThisQuantity.MeasurementType == input.ThatQuantity.MeasurementType;
    }
}