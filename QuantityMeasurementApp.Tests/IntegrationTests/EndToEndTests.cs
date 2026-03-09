using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.Tests.IntegrationTests;

public class EndToEndTests
{
    private const double Epsilon = 1e-10;

    [Fact]
    public void LengthMeasurement_CompleteWorkflow_ShouldWorkCorrectly()
    {
        // Arrange
        var service = new GenericMeasurementService();
        
        // Act - Add measurements
        var length1 = new GenericQuantity<LengthUnit>(2.5, LengthUnit.Feet);
        var length2 = new GenericQuantity<LengthUnit>(30, LengthUnit.Inch);
        var length3 = new GenericQuantity<LengthUnit>(1, LengthUnit.Yard);
        
        service.AddMeasurement(length1);
        service.AddMeasurement(length2);
        service.AddMeasurement(length3);
        
        // Assert - Measurements added
        Assert.Equal(3, service.GetMeasurementsByType<LengthUnit>().Count);
        
        // Act - Compare measurements
        var compareResult = service.CompareMeasurements(
            new GenericQuantity<LengthUnit>(1, LengthUnit.Feet),
            new GenericQuantity<LengthUnit>(12, LengthUnit.Inch)
        );
        
        // Assert
        Assert.True(compareResult);
        
        // Act - Convert measurement
        var converted = service.ConvertMeasurement(length1, LengthUnit.Inch);
        
        // Assert
        Assert.Equal(30, converted.Value, 10);
        
        // Act - Add measurements
        var sum = service.AddMeasurements(length1, length2);
        
        // Assert
        Assert.Equal(5, sum.Value, 10);
    }

    [Fact]
    public void WeightMeasurement_CompleteWorkflow_ShouldWorkCorrectly()
    {
        // Arrange
        var service = new GenericMeasurementService();
        
        // Act - Add measurements
        var weight1 = new GenericQuantity<WeightUnit>(2.5, WeightUnit.Kilogram);
        var weight2 = new GenericQuantity<WeightUnit>(1500, WeightUnit.Gram);
        var weight3 = new GenericQuantity<WeightUnit>(5, WeightUnit.Pound);
        
        service.AddMeasurement(weight1);
        service.AddMeasurement(weight2);
        service.AddMeasurement(weight3);
        
        // Assert - Measurements added
        Assert.Equal(3, service.GetMeasurementsByType<WeightUnit>().Count);
        
        // Act - Compare measurements
        var compareResult = service.CompareMeasurements(
            new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram),
            new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram)
        );
        
        // Assert
        Assert.True(compareResult);
        
        // Act - Convert measurement
        var converted = service.ConvertMeasurement(weight1, WeightUnit.Gram);
        
        // Assert
        Assert.Equal(2500, converted.Value, 10);
        
        // Act - Add measurements
        var sum = service.AddMeasurements(weight1, weight2);
        
        // Assert
        Assert.Equal(4.0, sum.Value, 10);
    }

    [Fact]
    public void VolumeMeasurement_CompleteWorkflow_ShouldWorkCorrectly()
    {
        // Arrange
        var service = new GenericMeasurementService();
        
        // Act - Add measurements
        var volume1 = new GenericQuantity<VolumeUnit>(2.5, VolumeUnit.Litre);
        var volume2 = new GenericQuantity<VolumeUnit>(1500, VolumeUnit.Millilitre);
        var volume3 = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Gallon);
        
        service.AddMeasurement(volume1);
        service.AddMeasurement(volume2);
        service.AddMeasurement(volume3);
        
        // Assert - Measurements added
        Assert.Equal(3, service.GetMeasurementsByType<VolumeUnit>().Count);
        
        // Act - Compare measurements
        var compareResult = service.CompareMeasurements(
            new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre),
            new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre)
        );
        
        // Assert
        Assert.True(compareResult);
        
        // Act - Convert measurement
        var converted = service.ConvertMeasurement(volume1, VolumeUnit.Millilitre);
        
        // Assert
        Assert.Equal(2500, converted.Value, 10);
        
        // Act - Add measurements
        var sum = service.AddMeasurements(volume1, volume2);
        
        // Assert
        Assert.Equal(4.0, sum.Value, 10);
    }

    [Fact]
    public void MixedOperations_AllCategories_ShouldNotInterfere()
    {
        // Arrange
        var service = new GenericMeasurementService();
        
        // Act - Add mixed measurements
        service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        service.AddMeasurement(new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre));
        
        // Assert
        Assert.Equal(3, service.GetAllMeasurements().Count);
        Assert.Equal(1, service.GetMeasurementsByType<LengthUnit>().Count);
        Assert.Equal(1, service.GetMeasurementsByType<WeightUnit>().Count);
        Assert.Equal(1, service.GetMeasurementsByType<VolumeUnit>().Count);
    }

    [Fact]
    public void CrossCategory_Comparisons_ShouldAlwaysReturnFalse()
    {
        // Arrange
        var length = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var weight = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
        var volume = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        
        // Act & Assert
        Assert.False(length.Equals(weight));
        Assert.False(length.Equals(volume));
        Assert.False(weight.Equals(volume));
    }

    [Fact]
    public void ClearAllMeasurements_ShouldRemoveAllCategories()
    {
        // Arrange
        var service = new GenericMeasurementService();
        service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        service.AddMeasurement(new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre));
        
        // Act
        service.ClearAllMeasurements();
        
        // Assert
        Assert.Empty(service.GetAllMeasurements());
    }
}