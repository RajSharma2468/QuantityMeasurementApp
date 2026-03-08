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

    [Fact]
    public void LengthUnit_ConvertToBaseUnit_ShouldWorkCorrectly()
    {
        // Act & Assert
        Assert.Equal(1, LengthUnit.Feet.ConvertToBaseUnit(1));
        Assert.Equal(1, LengthUnit.Inch.ConvertToBaseUnit(12), 10);
        Assert.Equal(3, LengthUnit.Yard.ConvertToBaseUnit(1));
        Assert.True(Math.Abs(LengthUnit.Centimeter.ConvertToBaseUnit(30.48) - 1) < 0.01);
    }

    [Fact]
    public void LengthUnit_ConvertFromBaseUnit_ShouldWorkCorrectly()
    {
        // Act & Assert
        Assert.Equal(1, LengthUnit.Feet.ConvertFromBaseUnit(1));
        Assert.Equal(12, LengthUnit.Inch.ConvertFromBaseUnit(1), 10);
        Assert.Equal(1, LengthUnit.Yard.ConvertFromBaseUnit(3));
        Assert.True(Math.Abs(LengthUnit.Centimeter.ConvertFromBaseUnit(1) - 30.48) < 0.01);
    }

    [Fact]
    public void LengthUnit_Equals_ShouldCompareCorrectly()
    {
        // Arrange
        var feet1 = LengthUnit.Feet;
        var feet2 = LengthUnit.Feet;
        var inch = LengthUnit.Inch;
        
        // Assert
        Assert.True(feet1.Equals(feet2));
        Assert.False(feet1.Equals(inch));
        Assert.False(feet1.Equals(null));
    }
}