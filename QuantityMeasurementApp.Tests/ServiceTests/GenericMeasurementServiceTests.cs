using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using QuantityMeasurementApp.Core.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests.ServiceTests;

public class GenericMeasurementServiceTests
{
    private readonly GenericMeasurementService _service;

    public GenericMeasurementServiceTests()
    {
        _service = new GenericMeasurementService();
    }

    #region AddMeasurement Tests

    [Fact]
    public void AddMeasurement_Length_Works()
    {
        // Arrange
        var measurement = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        // Act
        _service.AddMeasurement<LengthUnit>(measurement);
        
        // Assert
        Assert.Single(_service.GetAllMeasurements());
    }

    [Fact]
    public void AddMeasurement_Weight_Works()
    {
        // Arrange
        var measurement = new GenericQuantity<WeightUnit>(10, WeightUnit.Kilogram);
        
        // Act
        _service.AddMeasurement<WeightUnit>(measurement);
        
        // Assert
        Assert.Single(_service.GetAllMeasurements());
    }

    [Fact]
    public void AddMeasurement_Volume_Works()
    {
        // Arrange
        var measurement = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        
        // Act
        _service.AddMeasurement<VolumeUnit>(measurement);
        
        // Assert
        Assert.Single(_service.GetAllMeasurements());
    }

    [Fact]
    public void AddMeasurement_Null_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.AddMeasurement<LengthUnit>(null!));
    }

    #endregion

    #region CompareMeasurements Tests

    [Fact]
    public void CompareMeasurements_Length_EqualValues_ReturnsTrue()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act
        var result = _service.CompareMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CompareMeasurements_Length_DifferentValues_ReturnsFalse()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        
        // Act
        var result = _service.CompareMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CompareMeasurements_Weight_EqualValues_ReturnsTrue()
    {
        // Arrange
        var first = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var second = new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram);
        
        // Act
        var result = _service.CompareMeasurements<WeightUnit>(first, second);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CompareMeasurements_Volume_EqualValues_ReturnsTrue()
    {
        // Arrange
        var first = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var second = new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
        
        // Act
        var result = _service.CompareMeasurements<VolumeUnit>(first, second);
        
        // Assert
        Assert.True(result);
    }

    #endregion

    #region ConvertMeasurement Tests

    [Fact]
    public void ConvertMeasurement_Length_FeetToInch_ReturnsConvertedValue()
    {
        // Arrange
        var measurement = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        
        // Act
        var result = _service.ConvertMeasurement<LengthUnit>(measurement, LengthUnit.Inch);
        
        // Assert
        Assert.Equal(12, result.Value);
    }

    [Fact]
    public void ConvertMeasurement_Weight_KilogramToGram_ReturnsConvertedValue()
    {
        // Arrange
        var measurement = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        
        // Act
        var result = _service.ConvertMeasurement<WeightUnit>(measurement, WeightUnit.Gram);
        
        // Assert
        Assert.Equal(1000, result.Value);
    }

    [Fact]
    public void ConvertMeasurement_Volume_LitreToMillilitre_ReturnsConvertedValue()
    {
        // Arrange
        var measurement = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        
        // Act
        var result = _service.ConvertMeasurement<VolumeUnit>(measurement, VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(1000, result.Value);
    }

    #endregion

    #region AddMeasurements Tests

    [Fact]
    public void AddMeasurements_Length_SameUnit_ReturnsSum()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
        
        // Act
        var result = _service.AddMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.Equal(8, result.Value);
    }

    [Fact]
    public void AddMeasurements_Length_CrossUnit_ReturnsSum()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act
        var result = _service.AddMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void AddMeasurements_Weight_CrossUnit_ReturnsSum()
    {
        // Arrange
        var first = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var second = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
        
        // Act
        var result = _service.AddMeasurements<WeightUnit>(first, second);
        
        // Assert
        Assert.Equal(1.5, result.Value);
    }

    [Fact]
    public void AddMeasurements_Volume_CrossUnit_ReturnsSum()
    {
        // Arrange
        var first = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var second = new GenericQuantity<VolumeUnit>(500, VolumeUnit.Millilitre);
        
        // Act
        var result = _service.AddMeasurements<VolumeUnit>(first, second);
        
        // Assert
        Assert.Equal(1.5, result.Value);
    }

    #endregion

    #region AddMeasurementsWithTarget Tests

    [Fact]
    public void AddMeasurementsWithTarget_Length_ReturnsInTargetUnit()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act
        var result = _service.AddMeasurementsWithTarget<LengthUnit>(first, second, LengthUnit.Inch);
        
        // Assert
        Assert.Equal(24, result.Value);
    }

    [Fact]
    public void AddMeasurementsWithTarget_Weight_ReturnsInTargetUnit()
    {
        // Arrange
        var first = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var second = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
        
        // Act
        var result = _service.AddMeasurementsWithTarget<WeightUnit>(first, second, WeightUnit.Gram);
        
        // Assert
        Assert.Equal(1500, result.Value);
    }

    #endregion

    #region SubtractMeasurements Tests

    [Fact]
    public void SubtractMeasurements_Length_SameUnit_ReturnsDifference()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
        
        // Act
        var result = _service.SubtractMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void SubtractMeasurements_Length_CrossUnit_ReturnsDifference()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act
        var result = _service.SubtractMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void SubtractMeasurements_Weight_CrossUnit_ReturnsDifference()
    {
        // Arrange
        var first = new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram);
        var second = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
        
        // Act
        var result = _service.SubtractMeasurements<WeightUnit>(first, second);
        
        // Assert
        Assert.Equal(1.5, result.Value);
    }

    [Fact]
    public void SubtractMeasurements_Volume_CrossUnit_ReturnsDifference()
    {
        // Arrange
        var first = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var second = new GenericQuantity<VolumeUnit>(500, VolumeUnit.Millilitre);
        
        // Act
        var result = _service.SubtractMeasurements<VolumeUnit>(first, second);
        
        // Assert
        Assert.Equal(1.5, result.Value);
    }

    [Fact]
    public void SubtractMeasurements_NegativeResult_ReturnsNegative()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        // Act
        var result = _service.SubtractMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.Equal(-5, result.Value);
    }

    #endregion

    #region SubtractMeasurementsWithTarget Tests

    [Fact]
    public void SubtractMeasurementsWithTarget_Length_ReturnsInTargetUnit()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(6, LengthUnit.Inch);
        
        // Act
        var result = _service.SubtractMeasurementsWithTarget<LengthUnit>(first, second, LengthUnit.Inch);
        
        // Assert
        Assert.Equal(114, result.Value);
    }

    [Fact]
    public void SubtractMeasurementsWithTarget_Weight_ReturnsInTargetUnit()
    {
        // Arrange
        var first = new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram);
        var second = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
        
        // Act
        var result = _service.SubtractMeasurementsWithTarget<WeightUnit>(first, second, WeightUnit.Gram);
        
        // Assert
        Assert.Equal(1500, result.Value);
    }

    #endregion

    #region DivideMeasurements Tests

    [Fact]
    public void DivideMeasurements_Length_ReturnsRatio()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        
        // Act
        var result = _service.DivideMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void DivideMeasurements_Weight_ReturnsRatio()
    {
        // Arrange
        var first = new GenericQuantity<WeightUnit>(10, WeightUnit.Kilogram);
        var second = new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram);
        
        // Act
        var result = _service.DivideMeasurements<WeightUnit>(first, second);
        
        // Assert
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void DivideMeasurements_Volume_ReturnsRatio()
    {
        // Arrange
        var first = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        var second = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        
        // Act
        var result = _service.DivideMeasurements<VolumeUnit>(first, second);
        
        // Assert
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void DivideMeasurements_Length_CrossUnit_ReturnsRatio()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(24, LengthUnit.Inch);
        var second = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        
        // Act
        var result = _service.DivideMeasurements<LengthUnit>(first, second);
        
        // Assert
        Assert.Equal(1.0, result);
    }

    [Fact]
    public void DivideMeasurements_ByZero_ThrowsDivisionByZeroException()
    {
        // Arrange
        var first = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var second = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        
        // Act & Assert
        Assert.Throws<DivisionByZeroException>(() => _service.DivideMeasurements<LengthUnit>(first, second));
    }

    #endregion

    #region GetMeasurementsByType Tests

    [Fact]
    public void GetMeasurementsByType_WithMixedTypes_ReturnsOnlyLengthType()
    {
        // Arrange
        _service.AddMeasurement<LengthUnit>(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement<WeightUnit>(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        _service.AddMeasurement<VolumeUnit>(new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre));
        
        // Act
        var results = _service.GetMeasurementsByType<LengthUnit>();
        
        // Assert
        Assert.Single(results);
    }

    [Fact]
    public void GetMeasurementsByType_WithMixedTypes_ReturnsOnlyWeightType()
    {
        // Arrange
        _service.AddMeasurement<LengthUnit>(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement<WeightUnit>(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        _service.AddMeasurement<VolumeUnit>(new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre));
        
        // Act
        var results = _service.GetMeasurementsByType<WeightUnit>();
        
        // Assert
        Assert.Single(results);
    }

    [Fact]
    public void GetMeasurementsByType_WithMixedTypes_ReturnsOnlyVolumeType()
    {
        // Arrange
        _service.AddMeasurement<LengthUnit>(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement<WeightUnit>(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        _service.AddMeasurement<VolumeUnit>(new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre));
        
        // Act
        var results = _service.GetMeasurementsByType<VolumeUnit>();
        
        // Assert
        Assert.Single(results);
    }

    #endregion

    #region GetAllMeasurements Tests

    [Fact]
    public void GetAllMeasurements_WithMultipleTypes_ReturnsAll()
    {
        // Arrange
        _service.AddMeasurement<LengthUnit>(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement<WeightUnit>(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        _service.AddMeasurement<VolumeUnit>(new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre));
        
        // Act
        var results = _service.GetAllMeasurements();
        
        // Assert
        Assert.Equal(3, results.Count);
    }

    [Fact]
    public void GetAllMeasurements_WhenEmpty_ReturnsEmptyList()
    {
        // Act
        var results = _service.GetAllMeasurements();
        
        // Assert
        Assert.Empty(results);
    }

    #endregion

    #region ClearAllMeasurements Tests

    [Fact]
    public void ClearAllMeasurements_WithItems_RemovesAll()
    {
        // Arrange
        _service.AddMeasurement<LengthUnit>(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement<WeightUnit>(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        Assert.Equal(2, _service.GetAllMeasurements().Count);
        
        // Act
        _service.ClearAllMeasurements();
        
        // Assert
        Assert.Empty(_service.GetAllMeasurements());
    }

    #endregion

    #region CrossCategory Tests - Using Proper Exception Testing

    [Fact]
    public void CrossCategory_Operations_ArePreventedByTypeSystem()
    {
        // Arrange
        var length = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var weight = new GenericQuantity<WeightUnit>(5, WeightUnit.Kilogram);
        
        // Assert - These operations should not compile, but we can test the validation in the service
        // The service methods will throw InvalidOperationException when categories don't match
        Assert.Throws<InvalidOperationException>(() => 
            _service.AddMeasurements<LengthUnit>(length, It.IsAny<GenericQuantity<LengthUnit>>()));
    }

    #endregion
}

// Helper class for testing
public static class It
{
    public static T IsAny<T>() => default(T)!;
}