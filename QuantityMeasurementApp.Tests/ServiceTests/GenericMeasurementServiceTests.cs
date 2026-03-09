using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using QuantityMeasurementApp.Core.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests.ServiceTests;

public class GenericMeasurementServiceTests
{
    private readonly GenericMeasurementService _service;

    public GenericMeasurementServiceTests()
    {
        _service = new GenericMeasurementService();
    }

    [Fact]
    public void AddMeasurement_Length_Works()
    {
        var m = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        _service.AddMeasurement(m);
        
        Assert.Single(_service.GetAllMeasurements());
    }

    [Fact]
    public void CompareMeasurements_Length_Equal_ReturnsTrue()
    {
        var a = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var b = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        
        Assert.True(_service.CompareMeasurements(a, b));
    }

    [Fact]
    public void ConvertMeasurement_Length_Works()
    {
        var m = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var r = _service.ConvertMeasurement(m, LengthUnit.Inch);
        
        Assert.Equal(12, r.Value);
    }

    [Fact]
    public void AddMeasurements_Length_Works()
    {
        var a = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
        var b = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        var r = _service.AddMeasurements(a, b);
        
        Assert.Equal(2, r.Value);
    }

    [Fact]
    public void SubtractMeasurements_Length_Works()
    {
        var a = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        var b = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
        var r = _service.SubtractMeasurements(a, b);
        
        Assert.Equal(1, r.Value);
    }

    [Fact]
    public void DivideMeasurements_Length_Works()
    {
        var a = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var b = new GenericQuantity<LengthUnit>(2, LengthUnit.Feet);
        var r = _service.DivideMeasurements(a, b);
        
        Assert.Equal(5.0, r);
    }

    [Fact]
    public void GetMeasurementsByType_ReturnsCorrectType()
    {
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        
        var results = _service.GetMeasurementsByType<LengthUnit>();
        Assert.Single(results);
    }

    [Fact]
    public void ClearAllMeasurements_RemovesAll()
    {
        _service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        _service.AddMeasurement(new GenericQuantity<WeightUnit>(100, WeightUnit.Kilogram));
        
        _service.ClearAllMeasurements();
        Assert.Empty(_service.GetAllMeasurements());
    }
}