using Xunit;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.Tests.ArchitectureTests;

public class ScalabilityTests
{
    [Fact]
    public void GenericQuantity_WithDifferentUnitTypes_ShouldWorkIndependently()
    {
        // Arrange
        var lengthQuantity = new GenericQuantity<LengthUnit>(10.5, LengthUnit.Feet);
        var weightQuantity = new GenericQuantity<WeightUnit>(75.5, WeightUnit.Kilogram);
        var volumeQuantity = new GenericQuantity<VolumeUnit>(5.5, VolumeUnit.Litre);
        
        // Act & Assert
        Assert.IsType<GenericQuantity<LengthUnit>>(lengthQuantity);
        Assert.IsType<GenericQuantity<WeightUnit>>(weightQuantity);
        Assert.IsType<GenericQuantity<VolumeUnit>>(volumeQuantity);
        
        Assert.NotEqual(lengthQuantity.GetType(), weightQuantity.GetType());
        Assert.NotEqual(weightQuantity.GetType(), volumeQuantity.GetType());
        Assert.NotEqual(lengthQuantity.GetType(), volumeQuantity.GetType());
    }

    [Fact]
    public void GenericMeasurementService_WithMultipleCategories_ShouldStoreAllTypes()
    {
        // Arrange
        var service = new GenericMeasurementService();
        var length1 = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var length2 = new GenericQuantity<LengthUnit>(5, LengthUnit.Yard);
        var weight1 = new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram);
        var weight2 = new GenericQuantity<WeightUnit>(50, WeightUnit.Pound);
        var volume1 = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        var volume2 = new GenericQuantity<VolumeUnit>(5, VolumeUnit.Gallon);
        
        // Act
        service.AddMeasurement(length1);
        service.AddMeasurement(length2);
        service.AddMeasurement(weight1);
        service.AddMeasurement(weight2);
        service.AddMeasurement(volume1);
        service.AddMeasurement(volume2);
        
        // Assert
        Assert.Equal(6, service.GetAllMeasurements().Count);
        Assert.Equal(2, service.GetMeasurementsByType<LengthUnit>().Count);
        Assert.Equal(2, service.GetMeasurementsByType<WeightUnit>().Count);
        Assert.Equal(2, service.GetMeasurementsByType<VolumeUnit>().Count);
    }

    [Fact]
    public void GenericQuantity_WithSameBaseValue_DifferentCategories_ShouldNotBeEqual()
    {
        // Arrange
        var length = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var weight = new GenericQuantity<WeightUnit>(0.3048, WeightUnit.Kilogram); // Approximately same numeric value
        var volume = new GenericQuantity<VolumeUnit>(0.3048, VolumeUnit.Litre); // Same numeric value
        
        // Act & Assert
        Assert.False(length.Equals(weight));
        Assert.False(length.Equals(volume));
        Assert.False(weight.Equals(volume));
    }

    [Fact]
    public void NewMeasurementCategory_CanBeAdded_Easily()
    {
        // This test demonstrates that adding a new measurement category (like Temperature)
        // would require minimal code changes - just a new unit class implementing IMeasurable
        
        // Arrange - Simulating a new TemperatureUnit class
        var temperatureQuantity = CreateTestTemperatureQuantity(25.5, "Celsius");
        
        // Act
        var baseValue = temperatureQuantity.ConvertToBaseUnit();
        
        // Assert
        Assert.NotNull(temperatureQuantity);
        Assert.True(baseValue > 0);
        Assert.Equal(25.5, temperatureQuantity.Value);
        Assert.Equal("°C", temperatureQuantity.Unit.GetUnitSymbol());
    }

    [Fact]
    public void AllMeasurementCategories_ShouldImplementSamePatterns()
    {
        // Arrange
        var lengthUnit = LengthUnit.Feet;
        var weightUnit = WeightUnit.Kilogram;
        var volumeUnit = VolumeUnit.Litre;
        
        // Act & Assert - All should implement IMeasurable
        Assert.IsAssignableFrom<IMeasurable>(lengthUnit);
        Assert.IsAssignableFrom<IMeasurable>(weightUnit);
        Assert.IsAssignableFrom<IMeasurable>(volumeUnit);
        
        // All should have conversion methods
        Assert.Equal(1.0, lengthUnit.GetConversionFactor());
        Assert.Equal(1.0, weightUnit.GetConversionFactor());
        Assert.Equal(1.0, volumeUnit.GetConversionFactor());
    }

    [Fact]
    public void GenericQuantity_WithAllCategories_ShouldSupportAllOperations()
    {
        // Arrange
        var length = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        var weight = new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram);
        var volume = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        
        // Act - Test equality (self)
        Assert.True(length.Equals(length));
        Assert.True(weight.Equals(weight));
        Assert.True(volume.Equals(volume));
        
        // Act - Test conversion
        var lengthConverted = length.ConvertTo(LengthUnit.Inch);
        var weightConverted = weight.ConvertTo(WeightUnit.Gram);
        var volumeConverted = volume.ConvertTo(VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(24, lengthConverted.Value);
        Assert.Equal(2000, weightConverted.Value);
        Assert.Equal(2000, volumeConverted.Value);
        
        // Act - Test addition
        var lengthSum = length.Add(new GenericQuantity<LengthUnit>(2, LengthUnit.Feet));
        var weightSum = weight.Add(new GenericQuantity<WeightUnit>(2, WeightUnit.Kilogram));
        var volumeSum = volume.Add(new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre));
        
        // Assert
        Assert.Equal(4, lengthSum.Value);
        Assert.Equal(4, weightSum.Value);
        Assert.Equal(4, volumeSum.Value);
    }

    // Helper method to simulate a new measurement category (Temperature)
    private GenericQuantity<TestTemperatureUnit> CreateTestTemperatureQuantity(double value, string unitName)
    {
        var unit = new TestTemperatureUnit(unitName);
        return new GenericQuantity<TestTemperatureUnit>(value, unit);
    }
}

// Test implementation of a new measurement category (Temperature)
public class TestTemperatureUnit : IMeasurable
{
    private readonly string _unitName;
    private readonly double _conversionFactor;
    private readonly string _unitSymbol;

    public TestTemperatureUnit(string unitName)
    {
        _unitName = unitName;
        (_conversionFactor, _unitSymbol) = unitName.ToLower() switch
        {
            "celsius" => (1.0, "°C"),
            "fahrenheit" => (0.5555555555555556, "°F"), // 1°F = 5/9 °C
            "kelvin" => (1.0, "K"), // For simplicity, using 1K = 1°C (actually 1K = -272.15°C)
            _ => (1.0, "°C")
        };
    }

    public double GetConversionFactor() => _conversionFactor;
    public double ConvertToBaseUnit(double value) => value * _conversionFactor;
    public double ConvertFromBaseUnit(double baseValue) => baseValue / _conversionFactor;
    public string GetUnitSymbol() => _unitSymbol;
    public string GetUnitName() => _unitName;
}