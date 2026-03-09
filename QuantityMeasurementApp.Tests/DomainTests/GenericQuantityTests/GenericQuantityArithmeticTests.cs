using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityArithmeticTests
{
    [Fact]
    public void Add_SameUnit_Length_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
        var result = q1.Add(q2);
        
        Assert.Equal(8, result.Value);
    }

    [Fact]
    public void Add_SameUnit_Weight_Works()
    {
        var w1 = new GenericQuantity<WeightUnit>(5, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(3, WeightUnit.Kilogram);
        var result = w1.Add(w2);
        
        Assert.Equal(8, result.Value);
    }

    [Fact]
    public void Add_SameUnit_Volume_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(5, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(3, VolumeUnit.Litre);
        var result = v1.Add(v2);
        
        Assert.Equal(8, result.Value);
    }

    [Fact]
    public void Add_DifferentUnits_Length_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        var result = q1.Add(q2);
        
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void Add_DifferentUnits_Weight_Works()
    {
        var w1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram);
        var result = w1.Add(w2);
        
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void Add_DifferentUnits_Volume_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
        var result = v1.Add(v2);
        
        Assert.Equal(2, result.Value);
    }

    [Fact]
    public void Add_WithTargetUnit_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(1, LengthUnit.Yard);
        var result = q1.Add(q2, LengthUnit.Inch);
        
        Assert.Equal(48, result.Value);
    }
}