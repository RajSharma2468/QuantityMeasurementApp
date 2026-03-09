using Xunit;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Units;
using System;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class LengthUnitTests
{
    [Fact]
    public void LengthUnit_Symbols_AreCorrect()
    {
        Assert.Equal("in", LengthUnit.Inch.GetUnitSymbol());
        Assert.Equal("ft", LengthUnit.Feet.GetUnitSymbol());
        Assert.Equal("yd", LengthUnit.Yard.GetUnitSymbol());
        Assert.Equal("cm", LengthUnit.Centimeter.GetUnitSymbol());
    }

    [Fact]
    public void LengthUnit_Names_AreCorrect()
    {
        Assert.Equal("Inch", LengthUnit.Inch.GetUnitName());
        Assert.Equal("Feet", LengthUnit.Feet.GetUnitName());
        Assert.Equal("Yard", LengthUnit.Yard.GetUnitName());
        Assert.Equal("Centimeter", LengthUnit.Centimeter.GetUnitName());
    }

    [Fact]
    public void LengthUnit_ConversionFactors_AreCorrect()
    {
        Assert.Equal(1.0, LengthUnit.Feet.GetConversionFactor());
        Assert.Equal(1.0 / 12.0, LengthUnit.Inch.GetConversionFactor());
        Assert.Equal(3.0, LengthUnit.Yard.GetConversionFactor());
    }

    [Fact]
    public void LengthUnit_Equals_Works()
    {
        Assert.True(LengthUnit.Feet.Equals(LengthUnit.Feet));
        Assert.False(LengthUnit.Feet.Equals(LengthUnit.Inch));
    }
}