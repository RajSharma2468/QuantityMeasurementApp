using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericQuantityEdgeCasesTests
{
    [Fact]
    public void Constructor_NaN_ThrowsException()
    {
        Assert.Throws<InvalidValueException>(() => 
            new GenericQuantity<LengthUnit>(double.NaN, LengthUnit.Feet));
    }

    [Fact]
    public void Constructor_Infinity_ThrowsException()
    {
        Assert.Throws<InvalidValueException>(() => 
            new GenericQuantity<LengthUnit>(double.PositiveInfinity, LengthUnit.Feet));
    }

    [Fact]
    public void Constructor_NullUnit_ThrowsException()
    {
        Assert.Throws<ArgumentNullException>(() => 
            new GenericQuantity<LengthUnit>(10, null!));
    }

    [Fact]
    public void ConvertTo_NullTarget_ThrowsException()
    {
        var q = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q.ConvertTo(null!));
    }

    [Fact]
    public void Add_Null_ThrowsException()
    {
        var q = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q.Add(null!));
    }

    [Fact]
    public void Subtract_Null_ThrowsException()
    {
        var q = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        Assert.Throws<ArgumentNullException>(() => q.Subtract(null!));
    }

    [Fact]
    public void ToString_ReturnsFormattedString()
    {
        var q = new GenericQuantity<WeightUnit>(1.5, WeightUnit.Kilogram);
        
        Assert.Equal("1.50 kg", q.ToString());
    }
}