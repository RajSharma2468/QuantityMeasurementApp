using FluentValidation.TestHelper;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Validators;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace QuantityMeasurementApp.Tests.ValidatorTests;

public class QuantityInputDTOValidatorTests
{
    private readonly QuantityInputDTOValidator _validator;

    public QuantityInputDTOValidatorTests()
    {
        _validator = new QuantityInputDTOValidator();
    }

    [Fact]
    public void Should_Have_Error_When_ThisQuantity_Is_Null()
    {
        // Arrange
        var model = new QuantityInputDTO
        {
            ThisQuantity = null!,
            ThatQuantity = new QuantityDTO { Value = 10, Unit = "Feet", MeasurementType = "LengthUnit" }
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ThisQuantity);
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
    public void Should_Not_Have_Error_When_Model_Is_Valid()
    {
        // Arrange
        var model = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 10, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 5, Unit = "Inches", MeasurementType = "LengthUnit" }
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}