using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityArithmeticTests
{
    private const double Epsilon = 1e-10;

    [Fact]
    public void Add_SameUnit_Length_ShouldSumCorrectly()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
        
        // Act
        var result = q1.Add(q2);
        
        // Assert
        Assert.Equal(8, result.Value, Epsilon);
        Assert.Equal(LengthUnit.Feet.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_SameUnit_Weight_ShouldSumCorrectly()
    {
        // Arrange
        var w1 = new GenericQuantity<WeightUnit>(5, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(3, WeightUnit.Kilogram);
        
        // Act
        var result = w1.Add(w2);
        
        // Assert
        Assert.Equal(8, result.Value, Epsilon);
        Assert.Equal(WeightUnit.Kilogram.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_SameUnit_Volume_ShouldSumCorrectly()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(5, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(3, VolumeUnit.Litre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(8, result.Value, Epsilon);
        Assert.Equal(VolumeUnit.Litre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_DifferentUnits_Length_ShouldSumInFirstUnit()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act
        var result = q1.Add(q2);
        
        // Assert
        Assert.Equal(2, result.Value, Epsilon);
        Assert.Equal(LengthUnit.Feet.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_DifferentUnits_Weight_ShouldSumInFirstUnit()
    {
        // Arrange
        var w1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram);
        
        // Act
        var result = w1.Add(w2);
        
        // Assert
        Assert.Equal(2, result.Value, Epsilon);
        Assert.Equal(WeightUnit.Kilogram.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_DifferentUnits_Volume_ShouldSumInFirstUnit()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
        
        // Act
        var result = v1.Add(v2);
        
        // Assert
        Assert.Equal(2, result.Value, Epsilon);
        Assert.Equal(VolumeUnit.Litre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_WithTargetUnit_Length_ShouldReturnInTargetUnit()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(1, LengthUnit.Yard);
        
        // Act
        var result = q1.Add(q2, LengthUnit.Inch);
        
        // Assert
        Assert.Equal(48, result.Value, Epsilon);
        Assert.Equal(LengthUnit.Inch.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_WithTargetUnit_Weight_ShouldReturnInTargetUnit()
    {
        // Arrange
        var w1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(1, WeightUnit.Pound);
        
        // Act
        var result = w1.Add(w2, WeightUnit.Gram);
        
        // Assert
        Assert.Equal(1453.592, result.Value, 0.001);
        Assert.Equal(WeightUnit.Gram.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_WithTargetUnit_Volume_ShouldReturnInTargetUnit()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Gallon);
        
        // Act
        var result = v1.Add(v2, VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(4785.41, result.Value, 0.01);
        Assert.Equal(VolumeUnit.Millilitre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Add_WithZero_ShouldReturnOriginal()
    {
        // Arrange
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Inch);
        
        // Act
        var result = q1.Add(q2);
        
        // Assert
        Assert.Equal(5, result.Value, Epsilon);
    }

    [Fact]
    public void Add_NegativeValues_ShouldSubtract()
    {
        // Arrange
        var q1 = new GenericQuantity<WeightUnit>(5, WeightUnit.Kilogram);
        var q2 = new GenericQuantity<WeightUnit>(-2000, WeightUnit.Gram);
        
        // Act
        var result = q1.Add(q2);
        
        // Assert
        Assert.Equal(3, result.Value, Epsilon);
    }
}