using Xunit;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using System;

namespace QuantityMeasurementApp.Tests.ServiceTests;

public class GenericMeasurementServiceVolumeTests
{
    private readonly GenericMeasurementService _service;

    public GenericMeasurementServiceVolumeTests()
    {
        _service = new GenericMeasurementService();
    }

    [Fact]
    public void AddMeasurement_Volume_Works()
    {
        var v = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        _service.AddMeasurement(v);
        
        Assert.Single(_service.GetAllMeasurements());
    }

    [Fact]
    public void CompareMeasurements_Volume_Equal_Works()
    {
        var a = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var b = new GenericQuantity<VolumeUnit>(1000, VolumeUnit.Millilitre);
        
        Assert.True(_service.CompareMeasurements(a, b));
    }

    [Fact]
    public void ConvertMeasurement_Volume_Works()
    {
        var v = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var r = _service.ConvertMeasurement(v, VolumeUnit.Millilitre);
        
        Assert.Equal(1000, r.Value);
    }

    [Fact]
    public void AddMeasurements_Volume_Works()
    {
        var a = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var b = new GenericQuantity<VolumeUnit>(500, VolumeUnit.Millilitre);
        var r = _service.AddMeasurements(a, b);
        
        Assert.Equal(1.5, r.Value);
    }

    [Fact]
    public void AddMeasurementsWithTarget_Volume_Works()
    {
        var a = new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre);
        var b = new GenericQuantity<VolumeUnit>(500, VolumeUnit.Millilitre);
        var r = _service.AddMeasurementsWithTarget(a, b, VolumeUnit.Millilitre);
        
        Assert.Equal(1500, r.Value);
    }

    [Fact]
    public void SubtractMeasurements_Volume_Works()
    {
        var a = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var b = new GenericQuantity<VolumeUnit>(500, VolumeUnit.Millilitre);
        var r = _service.SubtractMeasurements(a, b);
        
        Assert.Equal(1.5, r.Value);
    }

    [Fact]
    public void SubtractMeasurementsWithTarget_Volume_Works()
    {
        var a = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var b = new GenericQuantity<VolumeUnit>(500, VolumeUnit.Millilitre);
        var r = _service.SubtractMeasurementsWithTarget(a, b, VolumeUnit.Millilitre);
        
        Assert.Equal(1500, r.Value);
    }

    [Fact]
    public void DivideMeasurements_Volume_Works()
    {
        var a = new GenericQuantity<VolumeUnit>(10, VolumeUnit.Litre);
        var b = new GenericQuantity<VolumeUnit>(2, VolumeUnit.Litre);
        var r = _service.DivideMeasurements(a, b);
        
        Assert.Equal(5.0, r);
    }

    [Fact]
    public void GetMeasurementsByType_Volume_Works()
    {
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement(new GenericQuantity<VolumeUnit>(1, VolumeUnit.Litre));
        _service.AddMeasurement(new GenericQuantity<VolumeUnit>(2, VolumeUnit.Gallon));
        
        var results = _service.GetMeasurementsByType<VolumeUnit>();
        Assert.Equal(2, results.Count);
    }
}