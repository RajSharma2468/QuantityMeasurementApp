using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;

namespace QuantityMeasurementApp.Tests.IntegrationTests;

public class EndToEndTests
{
    [Fact]
    public void WeightMeasurement_CompleteWorkflow_ShouldWorkCorrectly()
    {
        // Arrange
        var service = new QuantityMeasurementService();
        
        // Act - Add measurements
        var weight1 = new QuantityWeight(2.5, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1500.0, WeightUnit.GRAM);
        var weight3 = new QuantityWeight(5.0, WeightUnit.POUND);
        
        service.AddWeightMeasurement(weight1);
        service.AddWeightMeasurement(weight2);
        service.AddWeightMeasurement(weight3);
        
        // Assert - Measurements added
        Assert.Equal(3, service.GetAllWeightMeasurements().Count);
        
        // Act - Compare measurements
        var compare1 = service.CompareWeightMeasurements(
            new QuantityWeight(2.5, WeightUnit.KILOGRAM),
            new QuantityWeight(2500.0, WeightUnit.GRAM)
        );
        
        // Assert
        Assert.True(compare1);
        
        // Act - Convert measurements
        var converted = service.ConvertWeight(weight1, WeightUnit.POUND);
        
        // Assert
        Assert.Equal(WeightUnit.POUND, converted.Unit);
        Assert.True(converted.Value > 0);
        
        // Act - Add two weights
        var sum = service.AddWeightMeasurements(weight1, weight2);
        
        // Assert
        Assert.Equal(4.0, sum.Value); // 2.5 kg + 1.5 kg = 4.0 kg
        Assert.Equal(WeightUnit.KILOGRAM, sum.Unit);
        
        // Act - Add with target unit
        var sumInPounds = service.AddWeightMeasurementsWithTarget(weight1, weight2, WeightUnit.POUND);
        
        // Assert
        Assert.Equal(WeightUnit.POUND, sumInPounds.Unit);
    }

    [Fact]
    public void MultipleCategories_Workflow_ShouldNotInterfere()
    {
        // Arrange
        var service = new QuantityMeasurementService();
        
        // Act - Add weight measurements
        service.AddWeightMeasurement(new QuantityWeight(1.0, WeightUnit.KILOGRAM));
        service.AddWeightMeasurement(new QuantityWeight(500.0, WeightUnit.GRAM));
        
        // Act - Add length measurements
        service.AddLengthMeasurement(new QuantityLength(1.0, LengthUnit.FEET));
        service.AddLengthMeasurement(new QuantityLength(12.0, LengthUnit.INCH));
        
        // Assert - Both categories have correct counts
        Assert.Equal(2, service.GetAllWeightMeasurements().Count);
        Assert.Equal(2, service.GetAllLengthMeasurements().Count);
        
        // Act - Perform operations on both categories
        var weightSum = service.AddWeightMeasurements(
            new QuantityWeight(1.0, WeightUnit.KILOGRAM),
            new QuantityWeight(500.0, WeightUnit.GRAM)
        );
        
        var lengthSum = service.AddLengthMeasurements(
            new QuantityLength(1.0, LengthUnit.FEET),
            new QuantityLength(12.0, LengthUnit.INCH)
        );
        
        // Assert - Both operations work correctly
        Assert.Equal(1.5, weightSum.Value);
        Assert.Equal(2.0, lengthSum.Value);
    }
}