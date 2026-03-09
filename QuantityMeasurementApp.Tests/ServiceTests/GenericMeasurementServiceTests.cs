using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.Tests.ServiceTests;

public class GenericMeasurementServiceTests
{
    private readonly GenericMeasurementService _service;

    public GenericMeasurementServiceTests()
    {
        _service = new GenericMeasurementService();
    }

    [Fact]
    public void AddMeasurement_ValidMeasurement_ShouldAddToList()
    {
        // Arrange
        var measurement = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        // Act
        _service.AddMeasurement(measurement);
        
        // Assert
        Assert.Single(_service.GetAllMeasurements());
        Assert.Single(_service.GetMeasurementsByType<LengthUnit>());
    }

    [Fact]
    public void AddMeasurement_NullMeasurement_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _service.AddMeasurement<LengthUnit>(null!));
    }

    [Fact]
    public void CompareMeasurements_EqualValues_ShouldReturnTrue()
    {
        // Arrange
        var m1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var m2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        // Act
        var result = _service.CompareMeasurements(m1, m2);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ConvertMeasurement_ValidConversion_ShouldReturnConvertedValue()
    {
        // Arrange
        var measurement = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        
        // Act
        var result = _service.ConvertMeasurement(measurement, LengthUnit.Inch);
        
        // Assert
        Assert.Equal(12, result.Value, 10);
    }

    [Fact]
    public void GetMeasurementsByType_ShouldReturnOnlySpecifiedType()
    {
        // Arrange
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        _service.AddMeasurement(new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre));
        
        // Act
        var lengthMeasurements = _service.GetMeasurementsByType<LengthUnit>();
        var weightMeasurements = _service.GetMeasurementsByType<WeightUnit>();
        var volumeMeasurements = _service.GetMeasurementsByType<VolumeUnit>();
        
        // Assert
        Assert.Single(lengthMeasurements);
        Assert.Single(weightMeasurements);
        Assert.Single(volumeMeasurements);
    }

    [Fact]
    public void ClearAllMeasurements_ShouldRemoveAll()
    {
        // Arrange
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        
        // Act
        _service.ClearAllMeasurements();
        
        // Assert
        Assert.Empty(_service.GetAllMeasurements());
    }
}