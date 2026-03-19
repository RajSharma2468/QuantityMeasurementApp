using FluentValidation.TestHelper;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Validators;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace QuantityMeasurementApp.Tests.ValidatorTests;

public class CompareQuantitiesValidatorTests
{
    private readonly CompareQuantitiesValidator _validator;

    public CompareQuantitiesValidatorTests()
    {
        _validator = new CompareQuantitiesValidator();
    }

    [Fact]
    public void Should_Have_Error_When_MeasurementTypes_Differ()
    {
        // Arrange
        var model = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 10, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 5, Unit = "Kilogram", MeasurementType = "WeightUnit" }
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x);
    }

    [Fact]
    public void Should_Not_Have_Error_For_Valid_Comparison()
    {
        // Arrange
        var model = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}