using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.Tests.ServiceTests;

public class GenericMeasurementServiceVolumeTests
{
    private readonly GenericMeasurementService _service;

    public GenericMeasurementServiceVolumeTests()
    {
        _service = new GenericMeasurementService();
    }

    [Fact]
    public void AddMeasurement_ValidVolume_ShouldAddToList()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(10.5, VolumeUnit.Litre);
        
        // Act
        _service.AddMeasurement(volume);
        
        // Assert
        Assert.Single(_service.GetAllMeasurements());
        Assert.Single(_service.GetMeasurementsByType<VolumeUnit>());
    }

    [Fact]
    public void CompareMeasurements_EqualVolumes_ShouldReturnTrue()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
        
        // Act
        var result = _service.CompareMeasurements(v1, v2);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ConvertMeasurement_VolumeConversion_ShouldReturnConvertedValue()
    {
        // Arrange
        var volume = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        
        // Act
        var result = _service.ConvertMeasurement(volume, VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(1000.0, result.Value, 10);
        Assert.Equal(VolumeUnit.Millilitre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void AddMeasurements_CrossUnitVolume_ShouldReturnCorrectSum()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);
        
        // Act
        var result = _service.AddMeasurements(v1, v2);
        
        // Assert
        Assert.Equal(1.5, result.Value, 10);
        Assert.Equal(VolumeUnit.Litre.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void AddMeasurementsWithTarget_SpecificUnit_ShouldReturnInTargetUnit()
    {
        // Arrange
        var v1 = new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre);
        var v2 = new GenericQuantity<VolumeUnit>(500.0, VolumeUnit.Millilitre);
        
        // Act
        var result = _service.AddMeasurementsWithTarget(v1, v2, VolumeUnit.Gallon);
        
        // Assert
        Assert.Equal(VolumeUnit.Gallon.GetUnitSymbol(), result.Unit.GetUnitSymbol());
        Assert.True(result.Value > 0);
    }

    [Fact]
    public void GetMeasurementsByType_ShouldReturnOnlyVolumeType()
    {
        // Arrange
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        _service.AddMeasurement(new GenericQuantity<VolumeUnit>(1.0, VolumeUnit.Litre));
        _service.AddMeasurement(new GenericQuantity<VolumeUnit>(2.0, VolumeUnit.Gallon));
        
        // Act
        var volumeMeasurements = _service.GetMeasurementsByType<VolumeUnit>();
        
        // Assert
        Assert.Equal(2, volumeMeasurements.Count);
    }
}