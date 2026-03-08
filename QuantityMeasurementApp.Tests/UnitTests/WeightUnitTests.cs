using Xunit;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class WeightUnitTests
{
    [Fact]
    public void WeightUnit_Properties_ShouldReturnCorrectValues()
    {
        // Arrange & Act
        var kg = WeightUnit.Kilogram;
        var g = WeightUnit.Gram;
        var lb = WeightUnit.Pound;
        
        // Assert
        Assert.Equal("kg", kg.GetUnitSymbol());
        Assert.Equal("Kilogram", kg.GetUnitName());
        
        Assert.Equal("g", g.GetUnitSymbol());
        Assert.Equal("Gram", g.GetUnitName());
        
        Assert.Equal("lb", lb.GetUnitSymbol());
        Assert.Equal("Pound", lb.GetUnitName());
    }

    [Fact]
    public void WeightUnit_ConversionFactors_ShouldBeCorrect()
    {
        // Assert
        Assert.Equal(1.0, WeightUnit.Kilogram.GetConversionFactor());
        Assert.Equal(0.001, WeightUnit.Gram.GetConversionFactor());
        Assert.Equal(0.453592, WeightUnit.Pound.GetConversionFactor());
    }

    [Fact]
    public void WeightUnit_ConvertToBaseUnit_ShouldWorkCorrectly()
    {
        // Act & Assert
        Assert.Equal(5, WeightUnit.Kilogram.ConvertToBaseUnit(5));
        Assert.Equal(1, WeightUnit.Gram.ConvertToBaseUnit(1000));
        Assert.Equal(0.453592, WeightUnit.Pound.ConvertToBaseUnit(1));
    }

    [Fact]
    public void WeightUnit_ConvertFromBaseUnit_ShouldWorkCorrectly()
    {
        // Act & Assert
        Assert.Equal(5, WeightUnit.Kilogram.ConvertFromBaseUnit(5));
        Assert.Equal(1000, WeightUnit.Gram.ConvertFromBaseUnit(1));
        Assert.True(Math.Abs(WeightUnit.Pound.ConvertFromBaseUnit(0.453592) - 1) < 1e-10);
    }

    [Fact]
    public void WeightUnit_Equals_ShouldCompareCorrectly()
    {
        // Arrange
        var kg1 = WeightUnit.Kilogram;
        var kg2 = WeightUnit.Kilogram;
        var g = WeightUnit.Gram;
        
        // Assert
        Assert.True(kg1.Equals(kg2));
        Assert.False(kg1.Equals(g));
        Assert.False(kg1.Equals(null));
    }
}