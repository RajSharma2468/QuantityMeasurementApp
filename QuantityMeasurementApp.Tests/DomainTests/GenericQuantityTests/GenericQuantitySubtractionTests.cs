using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantitySubtractionTests
{
    private const double Epsilon = 1e-10;

    [Fact]
    public void Subtract_SameUnit_Feet_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
        var result = q1.Subtract(q2);
        
        Assert.Equal(7, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_SameUnit_Kilogram_Works()
    {
        var w1 = new GenericQuantity<WeightUnit>(10, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(3, WeightUnit.Kilogram);
        var result = w1.Subtract(w2);
        
        Assert.Equal(7, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_SameUnit_Litre_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(3, VolumeUnit.Litre);
        var result = v1.Subtract(v2);
        
        Assert.Equal(7, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_CrossUnit_Feet_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        var result = q1.Subtract(q2);
        
        Assert.Equal(1, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_CrossUnit_Kilogram_Works()
    {
        var w1 = new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
        var result = w1.Subtract(w2);
        
        Assert.Equal(1.5, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_CrossUnit_Litre_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(500, VolumeUnit.Millilitre);
        var result = v1.Subtract(v2);
        
        Assert.Equal(1.5, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_WithTargetUnit_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(6, LengthUnit.Inch);
        var result = q1.Subtract(q2, LengthUnit.Inch);
        
        Assert.Equal(114, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_NegativeResult_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var result = q1.Subtract(q2);
        
        Assert.Equal(-5, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_Zero_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(120, LengthUnit.Inch);
        var result = q1.Subtract(q2);
        
        Assert.Equal(0, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_Null_Throws()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q1.Subtract(null!));
    }
}