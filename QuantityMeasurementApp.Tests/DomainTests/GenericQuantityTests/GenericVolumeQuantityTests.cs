using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericVolumeQuantityTests
{
    private const double Epsilon = 1e-10;

    [Fact]
    public void LitreToLitre_SameValue_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        
        // Act & Assert
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void LitreToLitre_DifferentValue_ShouldNotBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(2.0, VolumeUnit.Litre);
        
        // Act & Assert
        Assert.False(v1.Equals(v2));
    }

    [Fact]
    public void LitreToMillilitre_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        
        // Act & Assert
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void MillilitreToLitre_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        var v2 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        
        // Act & Assert
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void LitreToGallon_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(0.264172, VolumeUnit.Gallon);
        
        // Act & Assert
        Assert.True(Math.Abs(v1.ConvertToBaseUnit() - v2.ConvertToBaseUnit()) < 1e-5);
    }

    [Fact]
    public void GallonToLitre_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
        var v2 = new GenericQuantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
        
        // Act & Assert
        Assert.True(Math.Abs(v1.ConvertToBaseUnit() - v2.ConvertToBaseUnit()) < 1e-5);
    }

    [Fact]
    public void MillilitreToGallon_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        var v2 = new GenericQuantity<VolumeUnit>(0.264172, VolumeUnit.Gallon);
        
        // Act & Assert
        Assert.True(Math.Abs(v1.ConvertToBaseUnit() - v2.ConvertToBaseUnit()) < 1e-5);
    }

    [Fact]
    public void VolumeVsLength_Incompatible_ShouldNotBeEqual()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var length = new GenericQuantity<LengthUnit>(1.0, LengthUnit.Feet);
        
        // Act & Assert
        Assert.False(volume.Equals(length));
    }

    [Fact]
    public void VolumeVsWeight_Incompatible_ShouldNotBeEqual()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var weight = new GenericQuantity<WeightUnit>(1.0, WeightUnit.Kilogram);
        
        // Act & Assert
        Assert.False(volume.Equals(weight));
    }

    [Fact]
    public void ZeroValue_AcrossVolumeUnits_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(0.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(0.0, VolumeUnit.Millilitre);
        var v3 = new GenericQuantity<VolumeUnit>(0.0, VolumeUnit.Gallon);
        
        // Act & Assert
        Assert.True(v1.Equals(v2));
        Assert.True(v2.Equals(v3));
        Assert.True(v1.Equals(v3));
    }

    [Fact]
    public void NegativeVolume_AcrossUnits_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(-1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(-1000.0, VolumeUnit.Millilitre);
        
        // Act & Assert
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void Convert_LitreToMillilitre_ShouldReturnCorrectValue()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(2.5, VolumeUnit.Litre);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(2500.0, converted.Value, Epsilon);
        Assert.Equal(VolumeUnit.Millilitre.GetUnitSymbol(), converted.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Convert_MillilitreToLitre_ShouldReturnCorrectValue()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(2500.0, VolumeUnit.Millilitre);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Litre);
        
        // Assert
        Assert.Equal(2.5, converted.Value, Epsilon);
    }

    [Fact]
    public void Convert_GallonToLitre_ShouldReturnCorrectValue()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(2.0, VolumeUnit.Gallon);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Litre);
        
        // Assert
        Assert.Equal(7.57082, converted.Value, 0.0001);
    }

    [Fact]
    public void Convert_LitreToGallon_ShouldReturnCorrectValue()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(7.57082, VolumeUnit.Litre);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Gallon);
        
        // Assert
        Assert.Equal(2.0, converted.Value, 0.0001);
    }

    [Fact]
    public void Convert_MillilitreToGallon_ShouldReturnCorrectValue()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(3785.41, VolumeUnit.Millilitre);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Gallon);
        
        // Assert
        Assert.Equal(1.0, converted.Value, 0.0001);
    }

    [Fact]
    public void Convert_SameUnit_ShouldReturnSameValue()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(5.0, VolumeUnit.Litre);
        
        // Act
        var converted = volume.ConvertTo(VolumeUnit.Litre);
        
        // Assert
        Assert.Equal(volume.Value, converted.Value, Epsilon);
    }

    [Fact]
    public void Convert_RoundTrip_ShouldPreserveValue()
    {
        // Arrange
        var original = new GenericQuantity<VolumeUnit>(1.5, VolumeUnit.Litre);
        
        // Act
        var toMillilitre = original.ConvertTo(VolumeUnit.Millilitre);
        var backToLitre = toMillilitre.ConvertTo(VolumeUnit.Litre);
        
        // Assert
        Assert.Equal(original.Value, backToLitre.Value, Epsilon);
    }

    [Fact]
    public void Add_SameUnit_LitrePlusLitre_ShouldSumCorrectly()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(2.0, VolumeUnit.Litre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(3.0, result.Value, Epsilon);
        Assert.Equal(VolumeUnit.Litre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_CrossUnit_LitrePlusMillilitre_ShouldSumInFirstUnit()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(2.0, result.Value, Epsilon);
        Assert.Equal(VolumeUnit.Litre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_CrossUnit_MillilitrePlusLitre_ShouldSumInFirstUnit()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        var v2 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(2000.0, result.Value, Epsilon);
        Assert.Equal(VolumeUnit.Millilitre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_CrossUnit_GallonPlusLitre_ShouldSumInFirstUnit()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
        var v2 = new GenericQuantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(2.0, result.Value, 0.0001);
        Assert.Equal(VolumeUnit.Gallon.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_ExplicitTargetUnit_Litre_ShouldReturnInLitres()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        
        // Act
        var result = v1.Add(v2, VolumeUnit.Litre);
        
        // Assert
        Assert.Equal(2.0, result.Value, Epsilon);
        Assert.Equal(VolumeUnit.Litre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_ExplicitTargetUnit_Millilitre_ShouldReturnInMillilitres()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        
        // Act
        var result = v1.Add(v2, VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(2000.0, result.Value, Epsilon);
        Assert.Equal(VolumeUnit.Millilitre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_ExplicitTargetUnit_Gallon_ShouldReturnInGallons()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(3.78541, VolumeUnit.Litre);
        
        // Act
        var result = v1.Add(v2, VolumeUnit.Gallon);
        
        // Assert
        Assert.Equal(2.0, result.Value, 0.0001);
        Assert.Equal(VolumeUnit.Gallon.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_WithZero_ShouldReturnOriginal()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(5.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(0.0, VolumeUnit.Millilitre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(5.0, result.Value, Epsilon);
    }

    [Fact]
    public void Add_NegativeValues_ShouldSubtract()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(5.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(-2000.0, VolumeUnit.Millilitre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(3.0, result.Value, Epsilon);
    }

    [Fact]
    public void Add_LargeValues_ShouldHandleCorrectly()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1e6, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1e6, VolumeUnit.Litre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(2e6, result.Value, Epsilon);
    }
}