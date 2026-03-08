using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityEqualityTests
{
    [Fact]
    public void SameUnitSameValue_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(10.5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(10.5, LengthUnit.Feet);
        
        // Act & Assert
        Assert.True(q1.Equals(q2));
        Assert.Equal(q1.GetHashCode(), q2.GetHashCode());
    }

    [Fact]
    public void DifferentUnitEquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act & Assert
        Assert.True(q1.Equals(q2));
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
    public void SameReference_ShouldBeEqual()
    {
        // Arrange
        var q = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var sameRef = q;
        
        // Act & Assert
        Assert.True(q.Equals(sameRef));
    }

    [Fact]
    public void ZeroValue_AcrossUnits_ShouldBeEqual()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Inch);
        var q3 = new GenericQuantity<WeightUnit>(0, WeightUnit.Kilogram);
        var q4 = new GenericQuantity<WeightUnit>(0, WeightUnit.Gram);
        
        // Act & Assert
        Assert.True(q1.Equals(q2));
        Assert.True(q3.Equals(q4));
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