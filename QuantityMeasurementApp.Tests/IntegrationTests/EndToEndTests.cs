using Xunit;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using System;

namespace QuantityMeasurementApp.Tests.IntegrationTests;

public class EndToEndTests
{
    [Fact]
    public void Length_CompleteWorkflow_Works()
    {
        var service = new GenericMeasurementService();
        
        var length1 = new GenericQuantity<LengthUnit>(2.5, LengthUnit.Feet);
        var length2 = new GenericQuantity<LengthUnit>(30, LengthUnit.Inch);
        
        service.AddMeasurement(length1);
        service.AddMeasurement(length2);
        
        Assert.Equal(2, service.GetAllMeasurements().Count);
        
        var compare = service.CompareMeasurements(
            new GenericQuantity<LengthUnit>(1, LengthUnit.Feet),
            new GenericQuantity<LengthUnit>(12, LengthUnit.Inch)
        );
        Assert.True(compare);
        
        var converted = service.ConvertMeasurement(length1, LengthUnit.Inch);
        Assert.Equal(30, converted.Value, 5);
        
        var sum = service.AddMeasurements(length1, length2);
        Assert.Equal(5, sum.Value, 5);
        
        var diff = service.SubtractMeasurements(length1, length2);
        Assert.Equal(0, diff.Value, 5);
        
        var ratio = service.DivideMeasurements(length1, length2);
        Assert.Equal(1.0, ratio, 5);
    }

    [Fact]
    public void Weight_CompleteWorkflow_Works()
    {
        var service = new GenericMeasurementService();
        
        var weight1 = new GenericQuantity<WeightUnit>(2.5, WeightUnit.Kilogram);
        var weight2 = new GenericQuantity<WeightUnit>(1500, WeightUnit.Gram);
        
        service.AddMeasurement(weight1);
        service.AddMeasurement(weight2);
        
        var compare = service.CompareMeasurements(
            new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram),
            new GenericQuantity<WeightUnit>(1000, WeightUnit.Gram)
        );
        Assert.True(compare);
        
        var converted = service.ConvertMeasurement(weight1, WeightUnit.Gram);
        Assert.Equal(2500, converted.Value, 5);
        
        var sum = service.AddMeasurements(weight1, weight2);
        Assert.Equal(4.0, sum.Value, 5);
        
        var diff = service.SubtractMeasurements(weight1, weight2);
        Assert.Equal(1.0, diff.Value, 5);
        
        var ratio = service.DivideMeasurements(weight1, weight2);
        Assert.Equal(1.6667, ratio, 0.001);
    }

    [Fact]
    public void Volume_CompleteWorkflow_Works()
    {
        var service = new GenericMeasurementService();
        
        var volume1 = new GenericQuantity<VolumeUnit>(2.5, VolumeUnit.Litre);
        var volume2 = new GenericQuantity<VolumeUnit>(1500, VolumeUnit.Millilitre);
        
        service.AddMeasurement(volume1);
        service.AddMeasurement(volume2);
        
        var compare = service.CompareMeasurements(
            new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre),
            new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre)
        );
        Assert.True(compare);
        
        var converted = service.ConvertMeasurement(volume1, VolumeUnit.Millilitre);
        Assert.Equal(2500, converted.Value, 5);
        
        var sum = service.AddMeasurements(volume1, volume2);
        Assert.Equal(4.0, sum.Value, 5);
        
        var diff = service.SubtractMeasurements(volume1, volume2);
        Assert.Equal(1.0, diff.Value, 5);
        
        var ratio = service.DivideMeasurements(volume1, volume2);
        Assert.Equal(1.6667, ratio, 0.001);
    }

    [Fact]
    public void ClearAllMeasurements_Works()
    {
        var service = new GenericMeasurementService();
        
        service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        
        Assert.Equal(2, service.GetAllMeasurements().Count);
        
        service.ClearAllMeasurements();
        Assert.Empty(service.GetAllMeasurements());
    }
}