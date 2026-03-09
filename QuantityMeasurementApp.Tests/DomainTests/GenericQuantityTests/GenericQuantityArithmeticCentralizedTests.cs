using Xunit;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityArithmeticCentralizedTests
{
    private const double Epsilon = 1e-10;

    #region Validation Consistency Tests

    [Fact]
    public void AllOperations_RejectNullOperand_WithSameException()
    {
        var q = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q.Add(null!));
        Assert.Throws<ArgumentNullException>(() => q.Subtract(null!));
        Assert.Throws<ArgumentNullException>(() => q.Divide(null!));
    }

    [Fact]
    public void AllOperations_RejectNaNValue_WithSameException()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(double.NaN, LengthUnit.Feet);
        
        Assert.Throws<InvalidValueException>(() => q1.Add(q2));
        Assert.Throws<InvalidValueException>(() => q1.Subtract(q2));
        Assert.Throws<InvalidValueException>(() => q1.Divide(q2));
    }

    [Fact]
    public void AllOperations_RejectInfinityValue_WithSameException()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(double.PositiveInfinity, LengthUnit.Feet);
        
        Assert.Throws<InvalidValueException>(() => q1.Add(q2));
        Assert.Throws<InvalidValueException>(() => q1.Subtract(q2));
        Assert.Throws<InvalidValueException>(() => q1.Divide(q2));
    }

    #endregion

    #region Operation-Specific Tests for Supported Units

    [Fact]
    public void Add_SameUnit_Length_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
        
        var result = q1.Add(q2);
        
        Assert.Equal(8, result.Value, Epsilon);
    }

    [Fact]
    public void Add_CrossUnit_Length_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        var result = q1.Add(q2);
        
        Assert.Equal(2, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_SameUnit_Length_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
        
        var result = q1.Subtract(q2);
        
        Assert.Equal(2, result.Value, Epsilon);
    }

    [Fact]
    public void Divide_SameUnit_Length_Works()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        
        var result = q1.Divide(q2);
        
        Assert.Equal(5.0, result, Epsilon);
    }

    [Fact]
    public void Divide_ByZero_Throws()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        
        Assert.Throws<DivisionByZeroException>(() => q1.Divide(q2));
    }

    #endregion

    #region Immutability Tests

    [Fact]
    public void Add_DoesNotModifyOriginal()
    {
        var original = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var other = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        double originalValue = original.Value;
        
        var result = original.Add(other);
        
        Assert.Equal(originalValue, original.Value);
        Assert.NotEqual(result.Value, original.Value);
    }

    [Fact]
    public void Subtract_DoesNotModifyOriginal()
    {
        var original = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var other = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        double originalValue = original.Value;
        
        var result = original.Subtract(other);
        
        Assert.Equal(originalValue, original.Value);
        Assert.NotEqual(result.Value, original.Value);
    }

    [Fact]
    public void Divide_DoesNotModifyOriginal()
    {
        var original = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var other = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        double originalValue = original.Value;
        
        var result = original.Divide(other);
        
        Assert.Equal(originalValue, original.Value);
        Assert.Equal(2.0, result);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Add_WithZero_ReturnsOriginal()
    {
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        
        var result = q1.Add(q2);
        
        Assert.Equal(5, result.Value, Epsilon);
    }

    [Fact]
    public void Subtract_WithZero_ReturnsOriginal()
    {
        var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Feet);
        
        var result = q1.Subtract(q2);
        
        Assert.Equal(5, result.Value, Epsilon);
    }

    [Fact]
    public void Add_WithTarget_NullTarget_Throws()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q1.Add(q2, null!));
    }

    [Fact]
    public void Subtract_WithTarget_NullTarget_Throws()
    {
        var q1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var q2 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q1.Subtract(q2, null!));
    }

    #endregion
}