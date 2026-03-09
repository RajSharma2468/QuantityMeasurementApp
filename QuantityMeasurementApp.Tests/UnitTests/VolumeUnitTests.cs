using Xunit;
using QuantityMeasurementApp.Core.Domain.Units;
using System;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class VolumeUnitTests
{
    [Fact]
    public void VolumeUnit_Symbols_AreCorrect()
    {
        Assert.Equal("L", VolumeUnit.Litre.GetUnitSymbol());
        Assert.Equal("mL", VolumeUnit.Millilitre.GetUnitSymbol());
        Assert.Equal("gal", VolumeUnit.Gallon.GetUnitSymbol());
    }

    [Fact]
    public void VolumeUnit_Names_AreCorrect()
    {
        Assert.Equal("Litre", VolumeUnit.Litre.GetUnitName());
        Assert.Equal("Millilitre", VolumeUnit.Millilitre.GetUnitName());
        Assert.Equal("Gallon", VolumeUnit.Gallon.GetUnitName());
    }

    [Fact]
    public void VolumeUnit_ConversionFactors_AreCorrect()
    {
        Assert.Equal(1.0, VolumeUnit.Litre.GetConversionFactor());
        Assert.Equal(0.001, VolumeUnit.Millilitre.GetConversionFactor());
        Assert.Equal(3.78541, VolumeUnit.Gallon.GetConversionFactor());
    }

    [Fact]
    public void VolumeUnit_Equals_Works()
    {
        Assert.True(VolumeUnit.Litre.Equals(VolumeUnit.Litre));
        Assert.False(VolumeUnit.Litre.Equals(VolumeUnit.Millilitre));
    }
}