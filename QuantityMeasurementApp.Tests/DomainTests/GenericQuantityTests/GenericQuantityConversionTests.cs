using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityConversionTests
{
    private const double Epsilon = 1e-10;

    [Fact]
    public void Length_FeetToInch_ShouldConvertCorrectly()
    {
        // Arrange
        var length = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        
        // Act
        var converted = length.ConvertTo(LengthUnit.Inch);
        
        // Assert
        Assert.Equal(60, converted.Value, Epsilon);
        Assert.Equal(LengthUnit.Inch.GetUnitSymbol(), converted.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Length_FeetToYard_ShouldConvertCorrectly()
    {
        // Arrange
        var length = new GenericQuantity<LengthUnit>(9, LengthUnit.Feet);
        
        // Act
        var converted = length.ConvertTo(LengthUnit.Yard);
        
        // Assert
        Assert.Equal(3, converted.Value, Epsilon);
    }

    [Fact]
    public void Length_FeetToCentimeter_ShouldConvertCorrectly()
    {
        // Arrange
        var length = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        
        // Act
        var converted = length.ConvertTo(LengthUnit.Centimeter);
        
        // Assert
        Assert.True(Math.Abs(converted.Value - 30.48) < 0.01);
    }

    [Fact]
    public void Weight_KilogramToGram_ShouldConvertCorrectly()
    {
        // Arrange
        var weight = new GenericQuantity<WeightUnit>(2.5, WeightUnit.Kilogram);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.Gram);
        
        // Assert
        Assert.Equal(2500, converted.Value, Epsilon);
    }

    [Fact]
    public void Weight_KilogramToPound_ShouldConvertCorrectly()
    {
        // Arrange
        var weight = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.Pound);
        
        // Assert
        Assert.True(Math.Abs(converted.Value - 2.20462) < 0.0001);
    }

    [Fact]
    public void Weight_PoundToGram_ShouldConvertCorrectly()
    {
        // Arrange
        var weight = new GenericQuantity<WeightUnit>(1, WeightUnit.Pound);
        
        // Act
        var converted = weight.ConvertTo(WeightUnit.Gram);
        
        // Assert
        Assert.True(Math.Abs(converted.Value - 453.592) < 0.001);
    }

    [Fact]
    public void Volume_LitreToMillilitre_ShouldConvertCorrectly()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(2.5, VolumeUnit.Litre);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(2500, converted.Value, Epsilon);
    }

    [Fact]
    public void Volume_LitreToGallon_ShouldConvertCorrectly()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Gallon);
        
        // Assert
        Assert.Equal(1, converted.Value, 0.0001);
    }

    [Fact]
    public void Volume_GallonToMillilitre_ShouldConvertCorrectly()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Gallon);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(3785.41, converted.Value, 0.01);
    }

    [Fact]
    public void Convert_SameUnit_ShouldReturnSameValue()
    {
        // Arrange
        var original = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        // Act
        var converted = original.ConvertTo(LengthUnit.Feet);
        
        // Assert
        Assert.Equal(original.Value, converted.Value, Epsilon);
    }

    [Fact]
    public void Convert_RoundTrip_ShouldPreserveValue()
    {
        // Arrange
        var original = new GenericQuantity<LengthUnit>(7.5, LengthUnit.Feet);
        
        // Act
        var toInches = original.ConvertTo(LengthUnit.Inch);
        var backToFeet = toInches.ConvertTo(LengthUnit.Feet);
        
        // Assert
        Assert.Equal(original.Value, backToFeet.Value, Epsilon);
    }
}