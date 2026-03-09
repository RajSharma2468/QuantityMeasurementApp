using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityDivisionTests
{
    [Fact]
    public void Divide_SameUnit_Feet_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        var result = q1.Divide(q2);
        
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Divide_SameUnit_Kilogram_Works()
    {
        var w1 = new GenericQuantity<WeightUnit>(10, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram);
        var result = w1.Divide(w2);
        
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Divide_SameUnit_Litre_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var result = v1.Divide(v2);
        
        Assert.Equal(5.0, result);
    }

    [Fact]
    public void Divide_CrossUnit_Length_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(24, LengthUnit.Inch);
        var q2 = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        var result = q1.Divide(q2);
        
        Assert.Equal(1.0, result);
    }

    [Fact]
    public void Divide_CrossUnit_Weight_Works()
    {
        var w1 = new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram);
        var w2 = new GenericQuantity<WeightUnit>(2000, WeightUnit.Gram);
        var result = w1.Divide(w2);
        
        Assert.Equal(1.0, result);
    }

    [Fact]
    public void Divide_CrossUnit_Volume_Works()
    {
        var v1 = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(2000, VolumeUnit.Millilitre);
        var result = v1.Divide(v2);
        
        Assert.Equal(1.0, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsException()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        
        Assert.Throws<DivisionByZeroException>(() => q1.Divide(q2));
    }

    [Fact]
    public void Divide_Null_ThrowsException()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q1.Divide(null!));
    }
}