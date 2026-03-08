using System;
using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class QuantityMeasurementServiceTests
{
    private readonly QuantityMeasurementService _service;

    public QuantityMeasurementServiceTests()
    {
        _service = new QuantityMeasurementService();
    }

    [Fact]
    public void AddWeightMeasurement_ValidMeasurement_ShouldAddToList()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act
        _service.AddWeightMeasurement(weight);
        
        // Assert
        var measurements = _service.GetAllWeightMeasurements();
        Assert.Single(measurements);
        Assert.True(weight.Equals(measurements[0]));
    }

    [Fact]
    public void AddWeightMeasurement_NullMeasurement_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.AddWeightMeasurement(null!));
    }

    [Fact]
    public void AddLengthMeasurement_ValidMeasurement_ShouldAddToList()
    {
        // Arrange
        var length = new QuantityLength(1.0, LengthUnit.FEET);
        
        // Act
        _service.AddLengthMeasurement(length);
        
        // Assert
        var measurements = _service.GetAllLengthMeasurements();
        Assert.Single(measurements);
        Assert.True(length.Equals(measurements[0]));
    }

    [Fact]
    public void CompareWeightMeasurements_EqualValues_ShouldReturnTrue()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        
        // Act
        var result = _service.CompareWeightMeasurements(weight1, weight2);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CompareWeightMeasurements_NullFirst_ShouldThrowArgumentNullException()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.CompareWeightMeasurements(null!, weight));
    }

    [Fact]
    public void ConvertWeight_ValidConversion_ShouldReturnConvertedValue()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act
        var result = _service.ConvertWeight(weight, WeightUnit.GRAM);
        
        // Assert
        Assert.Equal(1000.0, result.Value);
        Assert.Equal(WeightUnit.GRAM, result.Unit);
    }

    [Fact]
    public void AddWeightMeasurements_CrossUnit_ShouldReturnCorrectSum()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(500.0, WeightUnit.GRAM);
        
        // Act
        var result = _service.AddWeightMeasurements(weight1, weight2);
        
        // Assert
        Assert.Equal(1.5, result.Value);
        Assert.Equal(WeightUnit.KILOGRAM, result.Unit);
    }

    [Fact]
    public void AddWeightMeasurementsWithTarget_SpecificUnit_ShouldReturnInTargetUnit()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(500.0, WeightUnit.GRAM);
        
        // Act
        var result = _service.AddWeightMeasurementsWithTarget(weight1, weight2, WeightUnit.POUND);
        
        // Assert
        Assert.Equal(WeightUnit.POUND, result.Unit);
        Assert.True(result.Value > 0);
    }

    [Fact]
    public void ClearAllMeasurements_ShouldRemoveAllMeasurements()
    {
        // Arrange
        _service.AddWeightMeasurement(new QuantityWeight(1.0, WeightUnit.KILOGRAM));
        _service.AddLengthMeasurement(new QuantityLength(1.0, LengthUnit.FEET));
        
        // Act
        _service.ClearAllMeasurements();
        
        // Assert
        Assert.Empty(_service.GetAllWeightMeasurements());
        Assert.Empty(_service.GetAllLengthMeasurements());
    }
}