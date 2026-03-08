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
        Assert.Equal(LengthUnit.Inch.GetUnitSymbol(), converted.Unit.GetUnitSymbol());
        Assert.Equal(30, converted.Value, 10);
        
        // Act - Add measurements
        var sum = service.AddMeasurements(length1, length2);
        
        // Assert
        Assert.Equal(5, sum.Value, 10); // 2.5 ft + 2.5 ft = 5 ft
        
        // Act - Add with target unit
        var sumInYards = service.AddMeasurementsWithTarget(length1, length2, LengthUnit.Yard);
        
        // Assert
        Assert.Equal(LengthUnit.Yard.GetUnitSymbol(), sumInYards.Unit.GetUnitSymbol());
        Assert.True(Math.Abs(sumInYards.Value - 1.667) < 0.01);
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
        Assert.Equal(WeightUnit.Gram.GetUnitSymbol(), converted.Unit.GetUnitSymbol());
        Assert.Equal(2500, converted.Value, 10);
        
        // Act - Add measurements
        var sum = service.AddMeasurements(weight1, weight2);
        
        // Assert
        Assert.Equal(4.0, sum.Value, 10); // 2.5 kg + 1.5 kg = 4.0 kg
        
        // Act - Add with target unit
        var sumInPounds = service.AddMeasurementsWithTarget(weight1, weight2, WeightUnit.Pound);
        
        // Assert
        Assert.Equal(WeightUnit.Pound.GetUnitSymbol(), sumInPounds.Unit.GetUnitSymbol());
        Assert.True(sumInPounds.Value > 0);
    }

    [Fact]
    public void MixedOperations_ShouldNotInterfere()
    {
        // Arrange
        var service = new GenericMeasurementService();
        
        // Act - Add mixed measurements
        service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        service.AddMeasurement(new GenericQuantity<LengthUnit>(5, LengthUnit.Yard));
        service.AddMeasurement(new GenericQuantity<WeightUnit>(50, WeightUnit.Pound));
        
        // Assert
        Assert.Equal(4, service.GetAllMeasurements().Count);
        Assert.Equal(2, service.GetMeasurementsByType<LengthUnit>().Count);
        Assert.Equal(2, service.GetMeasurementsByType<WeightUnit>().Count);
        
        // Act - Perform length operations
        var lengthSum = service.AddMeasurements(
            new GenericQuantity<LengthUnit>(1, LengthUnit.Feet),
            new GenericQuantity<LengthUnit>(12, LengthUnit.Inch)
        );
        
        // Assert
        Assert.Equal(2, lengthSum.Value, Epsilon);
        
        // Act - Perform weight operations
        var weightSum = service.AddMeasurements(
            new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram),
            new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram)
        );
        
        // Assert
        Assert.Equal(2, weightSum.Value, Epsilon);
    }
}