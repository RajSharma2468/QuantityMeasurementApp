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
}