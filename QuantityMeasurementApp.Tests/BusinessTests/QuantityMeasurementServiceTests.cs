using Microsoft.Extensions.Logging;
using Moq;
using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Interfaces;
using Xunit;

namespace QuantityMeasurementApp.Tests.BusinessTests;

public class QuantityMeasurementServiceTests
{
    private readonly Mock<IQuantityMeasurementRepository> _mockRepo;
    private readonly Mock<ILogger<QuantityMeasurementService>> _mockLogger;
    private readonly QuantityMeasurementService _service;
    
    public QuantityMeasurementServiceTests()
    {
        _mockRepo = new Mock<IQuantityMeasurementRepository>();
        _mockLogger = new Mock<ILogger<QuantityMeasurementService>>();
        _service = new QuantityMeasurementService(_mockRepo.Object, _mockLogger.Object);
    }
    
    [Fact]
    public async Task CompareQuantities_ValidInput_ReturnsTrue()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };
        
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<QuantityMeasurement>()))
            .ReturnsAsync((QuantityMeasurement q) => q);
        
        // Act
        var result = await _service.CompareQuantitiesAsync(input);
        
        // Assert
        Assert.False(result.IsError);
        Assert.Equal("true", result.ResultString);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<QuantityMeasurement>()), Times.Once);
    }
    
    [Fact]
    public async Task CompareQuantities_DifferentTypes_ThrowsException()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 1, Unit = "Kilogram", MeasurementType = "WeightUnit" }
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            _service.CompareQuantitiesAsync(input));
    }
    
    [Fact]
    public async Task ConvertQuantities_ValidInput_ReturnsConvertedValue()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 0, Unit = "Inches", MeasurementType = "LengthUnit" }
        };
        
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<QuantityMeasurement>()))
            .ReturnsAsync((QuantityMeasurement q) => q);
        
        // Act
        var result = await _service.ConvertQuantitiesAsync(input);
        
        // Assert
        Assert.False(result.IsError);
        Assert.Equal(12, result.ResultValue);
        Assert.Equal("Inches", result.ResultUnit);
    }
    
    [Fact]
    public async Task AddQuantities_ValidInput_ReturnsSum()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };
        
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<QuantityMeasurement>()))
            .ReturnsAsync((QuantityMeasurement q) => q);
        
        // Act
        var result = await _service.AddQuantitiesAsync(input);
        
        // Assert
        Assert.False(result.IsError);
        Assert.Equal(2, result.ResultValue);
        Assert.Equal("Feet", result.ResultUnit);
    }
    
    [Fact]
    public async Task DivideQuantities_DivideByZero_ThrowsException()
    {
        // Arrange
        var input = new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 10, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 0, Unit = "Feet", MeasurementType = "LengthUnit" }
        };
        
        // Act & Assert
        await Assert.ThrowsAsync<DivideByZeroException>(() => 
            _service.DivideQuantitiesAsync(input));
    }
}