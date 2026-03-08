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
    public void CompareMeasurements_DifferentValues_ShouldReturnFalse()
    {
        // Arrange
        var m1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var m2 = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        
        // Act
        var result = _service.CompareMeasurements(m1, m2);
        
        // Assert
        Assert.False(result);
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
        Assert.Equal(LengthUnit.Inch.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void AddMeasurements_CrossUnit_ShouldReturnCorrectSum()
    {
        // Arrange
        var m1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var m2 = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
        
        // Act
        var result = _service.AddMeasurements(m1, m2);
        
        // Assert
        Assert.Equal(1.5, result.Value, 10);
        Assert.Equal(WeightUnit.Kilogram.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void AddMeasurementsWithTarget_SpecificUnit_ShouldReturnInTargetUnit()
    {
        // Arrange
        var m1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var m2 = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
        
        // Act
        var result = _service.AddMeasurementsWithTarget(m1, m2, WeightUnit.Pound);
        
        // Assert
        Assert.Equal(WeightUnit.Pound.GetUnitSymbol(), result.Unit.GetUnitSymbol());
        Assert.True(result.Value > 0);
    }

    [Fact]
    public void GetMeasurementsByType_ShouldReturnOnlySpecifiedType()
    {
        // Arrange
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(5, LengthUnit.Yard));
        _service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        
        // Act
        var lengthMeasurements = _service.GetMeasurementsByType<LengthUnit>();
        var weightMeasurements = _service.GetMeasurementsByType<WeightUnit>();
        
        // Assert
        Assert.Equal(2, lengthMeasurements.Count);
        Assert.Single(weightMeasurements);
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