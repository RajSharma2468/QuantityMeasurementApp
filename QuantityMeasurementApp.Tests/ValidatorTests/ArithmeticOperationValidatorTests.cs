using FluentValidation.TestHelper;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Validators;
using FluentValidation;
using FluentValidation.Results;
using Xunit;

namespace QuantityMeasurementApp.Tests.ValidatorTests;

public class ArithmeticOperationValidatorTests
{
    [Fact]
    public void Should_Have_Error_When_Dividing_By_Zero()
    {
        // Arrange
        var validator = new ArithmeticOperationValidator("divide");
        var model = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 10, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 0, Unit = "Feet", MeasurementType = "LengthUnit" }
        };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ThatQuantity.Value);
    }

    [Fact]
    public void Should_Not_Have_Error_For_Valid_Addition()
    {
        // Arrange
        var validator = new ArithmeticOperationValidator("add");
        var model = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_MeasurementTypes_Differ_For_Subtraction()
    {
        // Arrange
        var validator = new ArithmeticOperationValidator("subtract");
        var model = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 10, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 5, Unit = "Kilogram", MeasurementType = "WeightUnit" }
        };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x);
    }
}