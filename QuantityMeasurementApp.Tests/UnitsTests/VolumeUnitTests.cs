using Xunit;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class VolumeUnitTests
{
    private const double Epsilon = 1e-10;

    [Fact]
    public void VolumeUnit_Properties_ShouldReturnCorrectValues()
    {
        // Arrange & Act
        var litre = VolumeUnit.Litre;
        var millilitre = VolumeUnit.Millilitre;
        var gallon = VolumeUnit.Gallon;
        
        // Assert
        Assert.Equal("L", litre.GetUnitSymbol());
        Assert.Equal("Litre", litre.GetUnitName());
        
        Assert.Equal("mL", millilitre.GetUnitSymbol());
        Assert.Equal("Millilitre", millilitre.GetUnitName());
        
        Assert.Equal("gal", gallon.GetUnitSymbol());
        Assert.Equal("Gallon", gallon.GetUnitName());
    }

    [Fact]
    public void VolumeUnit_ConversionFactors_ShouldBeCorrect()
    {
        // Assert
        Assert.Equal(1.0, VolumeUnit.Litre.GetConversionFactor());
        Assert.Equal(0.001, VolumeUnit.Millilitre.GetConversionFactor());
        Assert.Equal(3.78541, VolumeUnit.Gallon.GetConversionFactor());
    }

    [Fact]
    public void VolumeUnit_ConvertToBaseUnit_ShouldWorkCorrectly()
    {
        // Act & Assert
        Assert.Equal(5.0, VolumeUnit.Litre.ConvertToBaseUnit(5.0));
        Assert.Equal(1.0, VolumeUnit.Millilitre.ConvertToBaseUnit(1000.0));
        Assert.Equal(3.78541, VolumeUnit.Gallon.ConvertToBaseUnit(1.0));
    }

    [Fact]
    public void VolumeUnit_ConvertFromBaseUnit_ShouldWorkCorrectly()
    {
        // Act & Assert
        Assert.Equal(5.0, VolumeUnit.Litre.ConvertFromBaseUnit(5.0));
        Assert.Equal(1000.0, VolumeUnit.Millilitre.ConvertFromBaseUnit(1.0));
        Assert.True(Math.Abs(VolumeUnit.Gallon.ConvertFromBaseUnit(3.78541) - 1.0) < Epsilon);
    }

    [Fact]
    public void VolumeUnit_Equals_ShouldCompareCorrectly()
    {
        // Arrange
        var litre1 = VolumeUnit.Litre;
        var litre2 = VolumeUnit.Litre;
        var millilitre = VolumeUnit.Millilitre;
        
        // Assert
        Assert.True(litre1.Equals(litre2));
        Assert.False(litre1.Equals(millilitre));
        Assert.False(litre1.Equals(null));
    }
}