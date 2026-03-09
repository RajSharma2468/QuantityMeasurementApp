using Xunit;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class LengthUnitTests
{
    [Fact]
    public void LengthUnit_Properties_ShouldReturnCorrectValues()
    {
        // Arrange & Act
        var inch = LengthUnit.Inch;
        var feet = LengthUnit.Feet;
        var yard = LengthUnit.Yard;
        var cm = LengthUnit.Centimeter;
        
        // Assert
        Assert.Equal("in", inch.GetUnitSymbol());
        Assert.Equal("Inch", inch.GetUnitName());
        
        Assert.Equal("ft", feet.GetUnitSymbol());
        Assert.Equal("Feet", feet.GetUnitName());
        
        Assert.Equal("yd", yard.GetUnitSymbol());
        Assert.Equal("Yard", yard.GetUnitName());
        
        Assert.Equal("cm", cm.GetUnitSymbol());
        Assert.Equal("Centimeter", cm.GetUnitName());
    }

    [Fact]
    public void LengthUnit_ConversionFactors_ShouldBeCorrect()
    {
        // Assert
        Assert.True(Math.Abs(LengthUnit.Inch.GetConversionFactor() - (1.0 / 12.0)) < 1e-10);
        Assert.Equal(1.0, LengthUnit.Feet.GetConversionFactor());
        Assert.Equal(3.0, LengthUnit.Yard.GetConversionFactor());
        Assert.True(Math.Abs(LengthUnit.Centimeter.GetConversionFactor() - (1.0 / 30.48)) < 1e-10);
    }
}