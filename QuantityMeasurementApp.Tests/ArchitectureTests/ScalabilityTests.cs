using Xunit;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.ArchitectureTests;

public class ScalabilityTests
{
    [Fact]
    public void DifferentUnitTypes_ShouldWorkIndependently()
    {
        var length = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        var weight = new GenericQuantity<WeightUnit>(75, WeightUnit.Kilogram);
        var volume = new GenericQuantity<VolumeUnit>(5, VolumeUnit.Litre);
        var temperature = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        
        Assert.IsType<GenericQuantity<LengthUnit>>(length);
        Assert.IsType<GenericQuantity<WeightUnit>>(weight);
        Assert.IsType<GenericQuantity<VolumeUnit>>(volume);
        Assert.IsType<GenericQuantity<TemperatureUnit>>(temperature);
    }

    [Fact]
    public void AllMeasurementCategories_ImplementIMeasurable()
    {
        Assert.IsAssignableFrom<IMeasurable>(LengthUnit.Feet);
        Assert.IsAssignableFrom<IMeasurable>(WeightUnit.Kilogram);
        Assert.IsAssignableFrom<IMeasurable>(VolumeUnit.Litre);
        Assert.IsAssignableFrom<IMeasurable>(TemperatureUnit.Celsius);
    }
}