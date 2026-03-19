using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuantityMeasurementAPILayer.Controllers;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Interfaces;
using Xunit;

namespace QuantityMeasurementApp.Tests.APITests;

public class QuantitiesControllerTests
{
    private readonly Mock<IQuantityMeasurementService> _mockService;
    private readonly Mock<ILogger<QuantitiesController>> _mockLogger;
    private readonly QuantitiesController _controller;

    public QuantitiesControllerTests()
    {
        _mockService = new Mock<IQuantityMeasurementService>();
        _mockLogger = new Mock<ILogger<QuantitiesController>>();
        _controller = new QuantitiesController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Compare_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };

        var expectedResult = new QuantityResultDTO
        {
            ThisValue = 1,
            ThisUnit = "Feet",
            ThatValue = 12,
            ThatUnit = "Inches",
            Operation = "compare",
            ResultString = "true",
            IsError = false
        };

        _mockService.Setup(s => s.CompareQuantitiesAsync(input))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.Compare(input);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<QuantityResultDTO>(okResult.Value);
        Assert.Equal("true", returnValue.ResultString);
    }

    [Fact]
    public async Task Compare_InvalidInput_ReturnsBadRequest()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 1, Unit = "Kilogram", MeasurementType = "WeightUnit" }
        };

        _mockService.Setup(s => s.CompareQuantitiesAsync(input))
            .ThrowsAsync(new InvalidOperationException("Cannot compare different measurement categories"));

        // Act
        var result = await _controller.Compare(input);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task Convert_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 0, Unit = "Inches", MeasurementType = "LengthUnit" }
        };

        var expectedResult = new QuantityResultDTO
        {
            ThisValue = 1,
            ThisUnit = "Feet",
            ThatValue = 0,
            ThatUnit = "Inches",
            Operation = "convert",
            ResultValue = 12,
            ResultUnit = "Inches",
            IsError = false
        };

        _mockService.Setup(s => s.ConvertQuantitiesAsync(input))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.Convert(input);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<QuantityResultDTO>(okResult.Value);
        Assert.Equal(12, returnValue.ResultValue);
    }

    [Fact]
    public async Task Add_ValidInput_ReturnsOkResult()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };

        var expectedResult = new QuantityResultDTO
        {
            ThisValue = 1,
            ThisUnit = "Feet",
            ThatValue = 12,
            ThatUnit = "Inches",
            Operation = "add",
            ResultValue = 2,
            ResultUnit = "Feet",
            IsError = false
        };

        _mockService.Setup(s => s.AddQuantitiesAsync(input))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.Add(input);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<QuantityResultDTO>(okResult.Value);
        Assert.Equal(2, returnValue.ResultValue);
    }

    [Fact]
    public async Task GetOperationHistory_ValidOperation_ReturnsOkResult()
    {
        // Arrange
        var operation = "compare";
        var expectedHistory = new List<QuantityResultDTO>
        {
            new QuantityResultDTO
            {
                ThisValue = 1,
                ThisUnit = "Feet",
                Operation = "compare",
                ResultString = "true"
            }
        };

        _mockService.Setup(s => s.GetOperationHistoryAsync(operation))
            .ReturnsAsync(expectedHistory);

        // Act
        var result = await _controller.GetOperationHistory(operation);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<QuantityResultDTO>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetOperationCount_ValidOperation_ReturnsOkResult()
    {
        // Arrange
        var operation = "compare";
        var expectedCount = 5L;

        _mockService.Setup(s => s.GetOperationCountAsync(operation))
            .ReturnsAsync(expectedCount);

        // Act
        var result = await _controller.GetOperationCount(operation);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<long>(okResult.Value);
        Assert.Equal(expectedCount, returnValue);
    }

    [Fact]
    public async Task GetErrorHistory_ReturnsOkResult()
    {
        // Arrange
        var expectedErrors = new List<QuantityResultDTO>
        {
            new QuantityResultDTO { IsError = true, ErrorMessage = "Test error" }
        };

        _mockService.Setup(s => s.GetErrorHistoryAsync())
            .ReturnsAsync(expectedErrors);

        // Act
        var result = await _controller.GetErrorHistory();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<QuantityResultDTO>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.True(returnValue[0].IsError);
    }

    [Fact]
    public async Task GetMeasurementsByType_ValidType_ReturnsOkResult()
    {
        // Arrange
        var type = "LengthUnit";
        var expectedResults = new List<QuantityResultDTO>
        {
            new QuantityResultDTO { ThisMeasurementType = "LengthUnit" }
        };

        _mockService.Setup(s => s.GetMeasurementsByTypeAsync(type))
            .ReturnsAsync(expectedResults);

        // Act
        var result = await _controller.GetMeasurementsByType(type);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<QuantityResultDTO>>(okResult.Value);
        Assert.Single(returnValue);
    }
}