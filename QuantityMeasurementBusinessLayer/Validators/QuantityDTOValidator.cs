using FluentValidation;
using QuantityMeasurementBusinessLayer.DTOs;

namespace QuantityMeasurementBusinessLayer.Validators;

public class QuantityDTOValidator : BaseValidator<QuantityDTO>
{
    public QuantityDTOValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required")
            .GreaterThanOrEqualTo(0).WithMessage("Value must be greater than or equal to 0")
            .LessThanOrEqualTo(1_000_000).WithMessage("Value must be less than or equal to 1,000,000");
        
        RuleFor(x => x.Unit)
            .NotEmpty().WithMessage("Unit is required")
            .MaximumLength(50).WithMessage("Unit must not exceed 50 characters")
            .Must((dto, unit) => BeValidUnit(unit, dto.MeasurementType))
                .WithMessage("Unit '{PropertyValue}' is not valid for the specified measurement type");
        
        RuleFor(x => x.MeasurementType)
            .NotEmpty().WithMessage("Measurement type is required")
            .MaximumLength(50).WithMessage("Measurement type must not exceed 50 characters")
            .Must(BeValidMeasurementType).WithMessage("Measurement type must be one of: LengthUnit, WeightUnit, VolumeUnit, TemperatureUnit");
    }
}