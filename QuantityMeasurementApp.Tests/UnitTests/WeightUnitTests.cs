using System;
using Xunit;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Domain.Quantities;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class WeightUnitTests
{
    [Fact]
    public void KilogramToKilogram_SameValue_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act & Assert
        Assert.True(weight1.Equals(weight2));
    }

    [Fact]
    public void KilogramToKilogram_DifferentValue_ShouldNotBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(2.0, WeightUnit.KILOGRAM);
        
        // Act & Assert
        Assert.False(weight1.Equals(weight2));
    }

    [Fact]
    public void KilogramToGram_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        
        // Act & Assert
        Assert.True(weight1.Equals(weight2));
    }

    [Fact]
    public void GramToKilogram_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        var weight2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act & Assert
        Assert.True(weight1.Equals(weight2));
    }

    [Fact]
    public void KilogramToPound_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(2.20462, WeightUnit.POUND);
        
        // Act & Assert
        // Use approximate equality due to floating point precision
        Assert.True(Math.Abs(weight1.ConvertToBaseUnit() - weight2.ConvertToBaseUnit()) < 1e-5);
    }

    [Fact]
    public void PoundToGram_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.POUND);
        var weight2 = new QuantityWeight(453.592, WeightUnit.GRAM);
        
        // Act & Assert
        Assert.True(Math.Abs(weight1.ConvertToBaseUnit() - weight2.ConvertToBaseUnit()) < 1e-5);
    }

    [Fact]
    public void WeightVsLength_Incompatible_ShouldNotBeEqual()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var length = new QuantityLength(1.0, LengthUnit.FEET);
        
        // Act & Assert
        Assert.False(weight.Equals(length));
        Assert.False(length.Equals(weight));
    }

    [Fact]
    public void NullComparison_ShouldReturnFalse()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act & Assert
        Assert.False(weight.Equals(null));
    }

    [Fact]
    public void SameReference_ShouldBeEqual()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var sameReference = weight;
        
        // Act & Assert
        Assert.True(weight.Equals(sameReference));
    }

    [Fact]
    public void ZeroValue_AcrossUnits_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(0.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(0.0, WeightUnit.GRAM);
        var weight3 = new QuantityWeight(0.0, WeightUnit.POUND);
        
        // Act & Assert
        Assert.True(weight1.Equals(weight2));
        Assert.True(weight2.Equals(weight3));
        Assert.True(weight1.Equals(weight3));
    }

    [Fact]
    public void NegativeWeight_AcrossUnits_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(-1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(-1000.0, WeightUnit.GRAM);
        
        // Act & Assert
        Assert.True(weight1.Equals(weight2));
    }

    [Fact]
    public void LargeWeightValue_ShouldMaintainPrecision()
    {
        // Arrange
        var weight1 = new QuantityWeight(1000000.0, WeightUnit.GRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.KILOGRAM);
        
        // Act & Assert
        Assert.True(weight1.Equals(weight2));
    }

    [Fact]
    public void SmallWeightValue_ShouldMaintainPrecision()
    {
        // Arrange
        var weight1 = new QuantityWeight(0.001, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1.0, WeightUnit.GRAM);
        
        // Act & Assert
        Assert.True(weight1.Equals(weight2));
    }

    [Fact]
    public void Convert_PoundToKilogram_ShouldReturnCorrectValue()
    {
        // Arrange
        var weight = new QuantityWeight(2.20462, WeightUnit.POUND);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.KILOGRAM);
        
        // Assert
        Assert.Equal(WeightUnit.KILOGRAM, converted.Unit);
        Assert.True(Math.Abs(converted.Value - 1.0) < 1e-5);
    }

    [Fact]
    public void Convert_KilogramToPound_ShouldReturnCorrectValue()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.POUND);
        
        // Assert
        Assert.Equal(WeightUnit.POUND, converted.Unit);
        Assert.True(Math.Abs(converted.Value - 2.20462) < 1e-5);
    }

    [Fact]
    public void Convert_SameUnit_ShouldReturnSameValue()
    {
        // Arrange
        var weight = new QuantityWeight(5.0, WeightUnit.KILOGRAM);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.KILOGRAM);
        
        // Assert
        Assert.True(weight.Equals(converted));
    }

    [Fact]
    public void Convert_ZeroValue_ShouldReturnZero()
    {
        // Arrange
        var weight = new QuantityWeight(0.0, WeightUnit.KILOGRAM);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.GRAM);
        
        // Assert
        Assert.Equal(0.0, converted.Value);
        Assert.Equal(WeightUnit.GRAM, converted.Unit);
    }

    [Fact]
    public void Convert_NegativeValue_ShouldPreserveSign()
    {
        // Arrange
        var weight = new QuantityWeight(-1.0, WeightUnit.KILOGRAM);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.GRAM);
        
        // Assert
        Assert.Equal(-1000.0, converted.Value);
    }

    [Fact]
    public void Convert_RoundTrip_ShouldPreserveValue()
    {
        // Arrange
        var original = new QuantityWeight(1.5, WeightUnit.KILOGRAM);
        
        // Act
        var toGram = original.ConvertTo(WeightUnit.GRAM);
        var backToKg = toGram.ConvertTo(WeightUnit.KILOGRAM);
        
        // Assert
        Assert.True(Math.Abs(original.Value - backToKg.Value) < 1e-10);
    }

    [Fact]
    public void Add_SameUnit_KilogramPlusKilogram_ShouldSumCorrectly()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(2.0, WeightUnit.KILOGRAM);
        
        // Act
        var result = weight1.Add(weight2);
        
        // Assert
        Assert.Equal(3.0, result.Value);
        Assert.Equal(WeightUnit.KILOGRAM, result.Unit);
    }

    [Fact]
    public void Add_CrossUnit_KilogramPlusGram_ShouldSumInFirstUnit()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        
        // Act
        var result = weight1.Add(weight2);
        
        // Assert
        Assert.Equal(2.0, result.Value);
        Assert.Equal(WeightUnit.KILOGRAM, result.Unit);
    }

    [Fact]
    public void Add_CrossUnit_PoundPlusKilogram_ShouldSumInFirstUnit()
    {
        // Arrange
        var weight1 = new QuantityWeight(2.20462, WeightUnit.POUND);
        var weight2 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        
        // Act
        var result = weight1.Add(weight2);
        
        // Assert
        Assert.Equal(WeightUnit.POUND, result.Unit);
        Assert.True(Math.Abs(result.Value - 4.40924) < 1e-5);
    }

    [Fact]
    public void Add_ExplicitTargetUnit_Gram_ShouldReturnInGrams()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        
        // Act
        var result = weight1.Add(weight2, WeightUnit.GRAM);
        
        // Assert
        Assert.Equal(2000.0, result.Value);
        Assert.Equal(WeightUnit.GRAM, result.Unit);
    }

    [Fact]
    public void Add_ExplicitTargetUnit_Pound_ShouldReturnInPounds()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1.0, WeightUnit.POUND);
        
        // Act
        var result = weight1.Add(weight2, WeightUnit.POUND);
        
        // Assert
        Assert.Equal(WeightUnit.POUND, result.Unit);
        Assert.True(Math.Abs(result.Value - 3.20462) < 1e-5);
    }

    [Fact]
    public void Add_Commutativity_ShouldHold()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        
        // Act
        var result1 = weight1.Add(weight2);
        var result2 = weight2.Add(weight1);
        
        // Assert - Results should be equivalent when converted to same unit
        var result2InKg = result2.ConvertTo(WeightUnit.KILOGRAM);
        Assert.True(Math.Abs(result1.Value - result2InKg.Value) < 1e-10);
    }

    [Fact]
    public void Add_WithZero_ShouldReturnOriginal()
    {
        // Arrange
        var weight1 = new QuantityWeight(5.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(0.0, WeightUnit.GRAM);
        
        // Act
        var result = weight1.Add(weight2);
        
        // Assert
        Assert.Equal(5.0, result.Value);
    }

    [Fact]
    public void Add_NegativeValues_ShouldSubtract()
    {
        // Arrange
        var weight1 = new QuantityWeight(5.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(-2000.0, WeightUnit.GRAM);
        
        // Act
        var result = weight1.Add(weight2);
        
        // Assert
        Assert.Equal(3.0, result.Value);
    }

    [Fact]
    public void Add_LargeValues_ShouldHandleCorrectly()
    {
        // Arrange
        var weight1 = new QuantityWeight(1e6, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1e6, WeightUnit.KILOGRAM);
        
        // Act
        var result = weight1.Add(weight2);
        
        // Assert
        Assert.Equal(2e6, result.Value);
    }

    [Fact]
    public void Constructor_InvalidValue_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new QuantityWeight(double.NaN, WeightUnit.KILOGRAM));
        Assert.Throws<ArgumentException>(() => new QuantityWeight(double.PositiveInfinity, WeightUnit.KILOGRAM));
    }

    [Fact]
    public void Constructor_InvalidUnit_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new QuantityWeight(1.0, (WeightUnit)999));
    }

    [Fact]
    public void GetHashCode_EqualObjects_ShouldBeEqual()
    {
        // Arrange
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        
        // Act & Assert
        Assert.Equal(weight1.GetHashCode(), weight2.GetHashCode());
    }

    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var weight = new QuantityWeight(1.5, WeightUnit.KILOGRAM);
        
        // Act & Assert
        Assert.Equal("1.50 kg", weight.ToString());
    }
}