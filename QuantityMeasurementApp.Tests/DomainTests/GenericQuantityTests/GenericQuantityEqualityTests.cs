using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using System;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityEqualityTests
{
    [Fact]
    public void SameUnitSameValue_Length_AreEqual()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.True(q1.Equals(q2));
        Assert.Equal(q1.GetHashCode(), q2.GetHashCode());
    }

    [Fact]
    public void SameUnitSameValue_Weight_AreEqual()
    {
        var w1 = new GenericQuantity<WeightUnit>(10, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(10, WeightUnit.Kilogram);
        
        Assert.True(w1.Equals(w2));
    }

    [Fact]
    public void SameUnitSameValue_Volume_AreEqual()
    {
        var v1 = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void DifferentUnits_Equivalent_Length_AreEqual()
    {
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        Assert.True(q1.Equals(q2));
    }

    [Fact]
    public void DifferentUnits_Equivalent_Weight_AreEqual()
    {
        var w1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram);
        
        Assert.True(w1.Equals(w2));
    }

    [Fact]
    public void DifferentUnits_Equivalent_Volume_AreEqual()
    {
        var v1 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
        
        Assert.True(v1.Equals(v2));
    }

    [Fact]
    public void DifferentValues_AreNotEqual()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(20, LengthUnit.Feet);
        
        Assert.False(q1.Equals(q2));
    }

    [Fact]
    public void Null_ReturnsFalse()
    {
        var q = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.False(q.Equals(null));
    }

    [Fact]
    public void ZeroValue_AcrossUnits_AreEqual()
    {
        var q1 = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Inch);
        
        Assert.True(q1.Equals(q2));
    }
}