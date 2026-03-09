using Xunit;
using QuantityMeasurementApp.Core.Domain.Units;
using System;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class WeightUnitTests
{
    [Fact]
    public void WeightUnit_Symbols_AreCorrect()
    {
        Assert.Equal("kg", WeightUnit.Kilogram.GetUnitSymbol());
        Assert.Equal("g", WeightUnit.Gram.GetUnitSymbol());
        Assert.Equal("lb", WeightUnit.Pound.GetUnitSymbol());
    }

    [Fact]
    public void WeightUnit_Names_AreCorrect()
    {
        Assert.Equal("Kilogram", WeightUnit.Kilogram.GetUnitName());
        Assert.Equal("Gram", WeightUnit.Gram.GetUnitName());
        Assert.Equal("Pound", WeightUnit.Pound.GetUnitName());
    }

    [Fact]
    public void WeightUnit_ConversionFactors_AreCorrect()
    {
        Assert.Equal(1.0, WeightUnit.Kilogram.GetConversionFactor());
        Assert.Equal(0.001, WeightUnit.Gram.GetConversionFactor());
        Assert.Equal(0.453592, WeightUnit.Pound.GetConversionFactor());
    }

    [Fact]
    public void WeightUnit_Equals_Works()
    {
        Assert.True(WeightUnit.Kilogram.Equals(WeightUnit.Kilogram));
        Assert.False(WeightUnit.Kilogram.Equals(WeightUnit.Gram));
    }
}