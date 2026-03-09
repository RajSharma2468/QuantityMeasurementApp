using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityEqualityTests
{
    [Fact]
    public void SameUnitSameValue_Length_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(10.5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(10.5, LengthUnit.Feet);
        
        // Act & Assert
        Assert.True(q1.Equals(q2));
        Assert.Equal(q1.GetHashCode(), q2.GetHashCode());
    }

    [Fact]
    public void SameUnitSameValue_Weight_ShouldBeEqual()
    {
        // Arrange
        var w1 = new GenericQuantity<WeightUnit>(10.5, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(10.5, WeightUnit.Kilogram);
        
        // Act & Assert
        Assert.True(w1.Equals(w2));
    }

    [Fact]
    public void SameUnitSameValue_Volume_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(10.5, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(10.5, VolumeUnit.Litre);
        
        // Act & Assert
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void DifferentUnitEquivalentValue_Length_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act & Assert
        Assert.True(q1.Equals(q2));
    }

    [Fact]
    public void DifferentUnitEquivalentValue_Weight_ShouldBeEqual()
    {
        // Arrange
        var w1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram);
        
        // Act & Assert
        Assert.True(w1.Equals(w2));
    }

    [Fact]
    public void DifferentUnitEquivalentValue_Volume_ShouldBeEqual()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
        
        // Act & Assert
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void DifferentValue_ShouldNotBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(20, LengthUnit.Feet);
        
        // Act & Assert
        Assert.False(q1.Equals(q2));
    }

    [Fact]
    public void NullComparison_ShouldReturnFalse()
    {
        // Arrange
        var q = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        // Act & Assert
        Assert.False(q.Equals(null));
    }

    [Fact]
    public void ZeroValue_AcrossUnits_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Inch);
        var w1 = new GenericQuantity<WeightUnit>(0, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(0, WeightUnit.Gram);
        var v1 = new GenericQuantity<VolumeUnit>(0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(0, VolumeUnit.Millilitre);
        
        // Act & Assert
        Assert.True(q1.Equals(q2));
        Assert.True(w1.Equals(w2));
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void NegativeValue_AcrossUnits_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(-1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(-12, LengthUnit.Inch);
        
        // Act & Assert
        Assert.True(q1.Equals(q2));
    }
}