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
        
        // Act & Assert
        Assert.IsType<GenericQuantity<LengthUnit>>(lengthQuantity);
        Assert.IsType<GenericQuantity<WeightUnit>>(weightQuantity);
        Assert.NotEqual(lengthQuantity.GetType(), weightQuantity.GetType());
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
        
        // Act
        service.AddMeasurement(length1);
        service.AddMeasurement(length2);
        service.AddMeasurement(weight1);
        service.AddMeasurement(weight2);
        
        // Assert
        Assert.Equal(4, service.GetAllMeasurements().Count);
        Assert.Equal(2, service.GetMeasurementsByType<LengthUnit>().Count);
        Assert.Equal(2, service.GetMeasurementsByType<WeightUnit>().Count);
    }

    [Fact]
    public void NewMeasurementCategory_CanBeAdded_Easily()
    {
        // This test demonstrates that adding a new measurement category (like Volume)
        // would require minimal code changes - just a new unit class implementing IMeasurable
        
        // Arrange - Simulating a new VolumeUnit class
        var volumeQuantity = CreateTestVolumeQuantity(5.5, "Liter");
        
        // Act
        var baseValue = volumeQuantity.ConvertToBaseUnit();
        
        // Assert
        Assert.NotNull(volumeQuantity);
        Assert.True(baseValue > 0);
    }

    // Helper method to simulate a new measurement category
    private GenericQuantity<TestVolumeUnit> CreateTestVolumeQuantity(double value, string unitName)
    {
        var unit = new TestVolumeUnit(unitName);
        return new GenericQuantity<TestVolumeUnit>(value, unit);
    }
}

// Test implementation of a new measurement category (Volume)
public class TestVolumeUnit : IMeasurable
{
    private readonly string _unitName;
    private readonly double _conversionFactor;

    public TestVolumeUnit(string unitName)
    {
        _unitName = unitName;
        _conversionFactor = unitName switch
        {
            "Liter" => 1.0,
            "Milliliter" => 0.001,
            "Gallon" => 3.78541,
            _ => 1.0
        };
    }

    public double GetConversionFactor() => _conversionFactor;
    public double ConvertToBaseUnit(double value) => value * _conversionFactor;
    public double ConvertFromBaseUnit(double baseValue) => baseValue / _conversionFactor;
    public string GetUnitSymbol() => _unitName == "Liter" ? "L" : _unitName == "Milliliter" ? "mL" : "gal";
    public string GetUnitName() => _unitName;
}