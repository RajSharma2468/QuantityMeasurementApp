using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using System;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityConversionTests
{
    [Fact]
    public void Convert_FeetToInch_Works()
    {
        var length = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var result = length.ConvertTo(LengthUnit.Inch);
        
        Assert.Equal(60, result.Value);
    }

    [Fact]
    public void Convert_FeetToYard_Works()
    {
        var length = new GenericQuantity<LengthUnit>(9, LengthUnit.Feet);
        var result = length.ConvertTo(LengthUnit.Yard);
        
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void Convert_KilogramToGram_Works()
    {
        var weight = new GenericQuantity<WeightUnit>(2.5, WeightUnit.Kilogram);
        var result = weight.ConvertTo(WeightUnit.Gram);
        
        Assert.Equal(2500, result.Value);
    }

    [Fact]
    public void Convert_LitreToMillilitre_Works()
    {
        var volume = new GenericQuantity<VolumeUnit>(2.5, VolumeUnit.Litre);
        var result = volume.ConvertTo(VolumeUnit.Millilitre);
        
        Assert.Equal(2500, result.Value);
    }

    [Fact]
    public void Convert_RoundTrip_Works()
    {
        var original = new GenericQuantity<LengthUnit>(7.5, LengthUnit.Feet);
        var toInches = original.ConvertTo(LengthUnit.Inch);
        var backToFeet = toInches.ConvertTo(LengthUnit.Feet);
        
        Assert.Equal(original.Value, backToFeet.Value, 5);
    }
}