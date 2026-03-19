using FluentValidation;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Validators;

public class QuantityInputDTOValidator : BaseValidator<QuantityInputDTO>
{
    private readonly QuantityDTOValidator _quantityValidator;
    
    public QuantityInputDTOValidator()
    {
        _quantityValidator = new QuantityDTOValidator();
        
        RuleFor(x => x.ThisQuantity)
            .NotNull().WithMessage("First quantity is required")
            .SetValidator(_quantityValidator);
        
        RuleFor(x => x.ThatQuantity)
            .NotNull().WithMessage("Second quantity is required")
            .SetValidator(_quantityValidator);
        
        // Custom validation for operations
        RuleFor(x => x)
            .Must(HaveCompatibleTypesForOperation)
            .When(x => x.ThisQuantity != null && x.ThatQuantity != null)
            .WithMessage("Cannot perform operations between different measurement types");
    }
    
    private bool HaveCompatibleTypesForOperation(QuantityInputDTO input)
    {
        if (input.ThisQuantity == null || input.ThatQuantity == null)
            return true;
            
        return input.ThisQuantity.MeasurementType == input.ThatQuantity.MeasurementType;
    }
}