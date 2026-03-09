using Xunit;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using System;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericVolumeQuantityTests
{
    [Fact]
    public void LitreToLitre_Equal_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void LitreToMillilitre_Equal_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
        
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void Convert_LitreToMillilitre_Works()
    {
        var v = new GenericQuantity<VolumeUnit>(2.5, VolumeUnit.Litre);
        var result = v.ConvertTo(VolumeUnit.Millilitre);
        
        Assert.Equal(2500, result.Value);
    }

    [Fact]
    public void Add_Volume_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(3, VolumeUnit.Litre);
        var result = v1.Add(v2);
        
        Assert.Equal(5, result.Value);
    }

    [Fact]
    public void Subtract_Volume_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(5, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var result = v1.Subtract(v2);
        
        Assert.Equal(3, result.Value);
    }

    [Fact]
    public void Divide_Volume_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var result = v1.Divide(v2);
        
        Assert.Equal(5.0, result);
    }
}