using FluentValidation.TestHelper;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Validators;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace QuantityMeasurementApp.Tests.ValidatorTests;

public class QuantityDTOValidatorTests
{
    private readonly QuantityDTOValidator _validator;

    public QuantityDTOValidatorTests()
    {
        _validator = new QuantityDTOValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Value_Is_Negative()
    {
        // Arrange
        var model = new QuantityDTO 
        { 
            Value = -1, 
            Unit = "Feet", 
            MeasurementType = "LengthUnit" 
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Value);
    }

    [Fact]
    public void Should_Have_Error_When_Unit_Is_Empty()
    {
        // Arrange
        var model = new QuantityDTO 
        { 
            Value = 10, 
            Unit = "", 
            MeasurementType = "LengthUnit" 
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Unit);
    }

    [Fact]
    public void Should_Have_Error_When_MeasurementType_Is_Invalid()
    {
        // Arrange
        var model = new QuantityDTO 
        { 
            Value = 10, 
            Unit = "Feet", 
            MeasurementType = "InvalidType" 
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MeasurementType);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Model_Is_Valid()
    {
        // Arrange
        var model = new QuantityDTO 
        { 
            Value = 10, 
            Unit = "Feet", 
            MeasurementType = "LengthUnit" 
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}