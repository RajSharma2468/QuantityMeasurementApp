using System;
using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.ValueObjectTests;

public class InchTests
{
    [Fact]
    public void InchToInch_SameValue_ShouldBeEqual()
    {
        // Arrange
        var length1 = new QuantityLength(1.0, LengthUnit.INCH);
        var length2 = new QuantityLength(1.0, LengthUnit.INCH);
        
        // Act & Assert
        Assert.True(length1.Equals(length2));
    }

    [Fact]
    public void InchToFeet_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var length1 = new QuantityLength(12.0, LengthUnit.INCH);
        var length2 = new QuantityLength(1.0, LengthUnit.FEET);
        
        // Act & Assert
        Assert.True(length1.Equals(length2));
    }

    [Fact]
    public void InchToYard_EquivalentValue_ShouldBeEqual()
    {
        // Arrange
        var length1 = new QuantityLength(36.0, LengthUnit.INCH);
        var length2 = new QuantityLength(1.0, LengthUnit.YARD);
        
        // Act & Assert
        Assert.True(length1.Equals(length2));
    }

    [Fact]
    public void InchToCentimeter_ShouldConvertCorrectly()
    {
        // Arrange
        var length = new QuantityLength(1.0, LengthUnit.INCH);
        
        // Act
        var converted = length.ConvertTo(LengthUnit.CENTIMETER);
        
        // Assert
        Assert.True(Math.Abs(converted.Value - 2.54) < 1e-2);
    }

    [Fact]
    public void Add_Inches_ShouldSumCorrectly()
    {
        // Arrange
        var length1 = new QuantityLength(2.0, LengthUnit.INCH);
        var length2 = new QuantityLength(3.0, LengthUnit.INCH);
        
        // Act
        var result = length1.Add(length2);
        
        // Assert
        Assert.Equal(5.0, result.Value);
        Assert.Equal(LengthUnit.INCH, result.Unit);
    }
}