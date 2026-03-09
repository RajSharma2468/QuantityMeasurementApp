using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Services;
using QuantityMeasurementApp.Core.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests.IntegrationTests;

public class TemperatureEndToEndTests
{
    [Fact]
    public void Temperature_AddMeasurements_ThrowsUnsupportedOperationException()
    {
        var service = new GenericMeasurementService();
        
        var temp1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var temp2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        service.AddMeasurement(temp1);
        service.AddMeasurement(temp2);
        
        Assert.Equal(2, service.GetMeasurementsByType<TemperatureUnit>().Count);
        
        Assert.Throws<UnsupportedOperationException>(() => service.AddMeasurements(temp1, temp2));
    }

    [Fact]
    public void Temperature_CompleteWorkflow_EqualityAndConversionOnly()
    {
        var service = new GenericMeasurementService();
        
        var temp1 = new GenericQuantity<TemperatureUnit>(0, TemperatureUnit.Celsius);
        var temp2 = new GenericQuantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit);
        var temp3 = new GenericQuantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin);
        
        service.AddMeasurement(temp1);
        service.AddMeasurement(temp2);
        service.AddMeasurement(temp3);
        
        Assert.Equal(3, service.GetMeasurementsByType<TemperatureUnit>().Count);
        
        var compare1 = service.CompareMeasurements(temp1, temp2);
        Assert.True(compare1);
        
        var compare2 = service.CompareMeasurements(temp1, temp3);
        Assert.True(compare2);
        
        var converted = service.ConvertMeasurement(temp1, TemperatureUnit.Fahrenheit);
        Assert.Equal(32, converted.Value, 5);
        
        var converted2 = service.ConvertMeasurement(temp1, TemperatureUnit.Kelvin);
        Assert.Equal(273.15, converted2.Value, 5);
    }

    [Fact]
    public void Temperature_AllConversions_WorkCorrectly()
    {
        var service = new GenericMeasurementService();
        
        var celsius = new GenericQuantity<TemperatureUnit>(25, TemperatureUnit.Celsius);
        
        var toF = service.ConvertMeasurement(celsius, TemperatureUnit.Fahrenheit);
        Assert.Equal(77, toF.Value, 5);
        
        var toK = service.ConvertMeasurement(celsius, TemperatureUnit.Kelvin);
        Assert.Equal(298.15, toK.Value, 5);
        
        var backToC = service.ConvertMeasurement(toF, TemperatureUnit.Celsius);
        Assert.Equal(25, backToC.Value, 5);
    }

    [Fact]
    public void Temperature_GetMeasurementsByType_ReturnsOnlyTemperature()
    {
        var service = new GenericMeasurementService();
        
        service.AddMeasurement(new GenericQuantity<LengthUnit>(10, LengthUnit.Feet));
        service.AddMeasurement(new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius));
        service.AddMeasurement(new GenericQuantity<WeightUnit>(50, WeightUnit.Kilogram));
        service.AddMeasurement(new GenericQuantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit));
        
        var tempMeasurements = service.GetMeasurementsByType<TemperatureUnit>();
        
        Assert.Equal(2, tempMeasurements.Count);
        foreach (var temp in tempMeasurements)
        {
            Assert.IsType<GenericQuantity<TemperatureUnit>>(temp);
        }
    }

    [Fact]
    public void Temperature_CrossCategoryComparison_ReturnsFalse()
    {
        var service = new GenericMeasurementService();
        
        var temp = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var length = new GenericQuantity<LengthUnit>(100, LengthUnit.Feet);
        
        // We can't directly compare different types, so we use Equals method which handles type checking
        bool areEqual = temp.Equals(length);
        Assert.False(areEqual);
    }

    [Fact]
    public void Temperature_Subtract_ThrowsUnsupportedOperationException()
    {
        var temp1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var temp2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => temp1.Subtract(temp2));
    }

    [Fact]
    public void Temperature_Divide_ThrowsUnsupportedOperationException()
    {
        var temp1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var temp2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => temp1.Divide(temp2));
    }

    [Fact]
    public void Temperature_AddWithTarget_ThrowsUnsupportedOperationException()
    {
        var temp1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var temp2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => temp1.Add(temp2, TemperatureUnit.Fahrenheit));
    }

    [Fact]
    public void CelsiusToFahrenheit_SpecificValues_ConvertCorrectly()
    {
        var testCases = new[]
        {
            new { Celsius = 0.0, Fahrenheit = 32.0 },
            new { Celsius = 100.0, Fahrenheit = 212.0 },
            new { Celsius = -40.0, Fahrenheit = -40.0 },
            new { Celsius = 37.0, Fahrenheit = 98.6 }
        };

        foreach (var test in testCases)
        {
            var celsius = new GenericQuantity<TemperatureUnit>(test.Celsius, TemperatureUnit.Celsius);
            var fahrenheit = celsius.ConvertTo(TemperatureUnit.Fahrenheit);
            Assert.Equal(test.Fahrenheit, fahrenheit.Value, 1);
        }
    }

    [Fact]
    public void FahrenheitToCelsius_SpecificValues_ConvertCorrectly()
    {
        var testCases = new[]
        {
            new { Fahrenheit = 32.0, Celsius = 0.0 },
            new { Fahrenheit = 212.0, Celsius = 100.0 },
            new { Fahrenheit = -40.0, Celsius = -40.0 },
            new { Fahrenheit = 98.6, Celsius = 37.0 }
        };

        foreach (var test in testCases)
        {
            var fahrenheit = new GenericQuantity<TemperatureUnit>(test.Fahrenheit, TemperatureUnit.Fahrenheit);
            var celsius = fahrenheit.ConvertTo(TemperatureUnit.Celsius);
            Assert.Equal(test.Celsius, celsius.Value, 1);
        }
    }

    [Fact]
    public void KelvinToCelsius_SpecificValues_ConvertCorrectly()
    {
        var testCases = new[]
        {
            new { Kelvin = 273.15, Celsius = 0.0 },
            new { Kelvin = 373.15, Celsius = 100.0 },
            new { Kelvin = 0.0, Celsius = -273.15 }
        };

        foreach (var test in testCases)
        {
            var kelvin = new GenericQuantity<TemperatureUnit>(test.Kelvin, TemperatureUnit.Kelvin);
            var celsius = kelvin.ConvertTo(TemperatureUnit.Celsius);
            Assert.Equal(test.Celsius, celsius.Value, 1);
        }
    }

    [Fact]
    public void AbsoluteZero_Conversions_AreCorrect()
    {
        var absoluteZeroC = new GenericQuantity<TemperatureUnit>(-273.15, TemperatureUnit.Celsius);
        var inKelvin = absoluteZeroC.ConvertTo(TemperatureUnit.Kelvin);
        Assert.Equal(0, inKelvin.Value, 5);
        
        var inFahrenheit = absoluteZeroC.ConvertTo(TemperatureUnit.Fahrenheit);
        Assert.Equal(-459.67, inFahrenheit.Value, 2);
    }

    [Fact]
    public void Temperature_Equality_WithDifferentUnits_Works()
    {
        var boilingC = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var boilingF = new GenericQuantity<TemperatureUnit>(212, TemperatureUnit.Fahrenheit);
        var boilingK = new GenericQuantity<TemperatureUnit>(373.15, TemperatureUnit.Kelvin);
        
        Assert.True(boilingC.Equals(boilingF));
        Assert.True(boilingC.Equals(boilingK));
        Assert.True(boilingF.Equals(boilingK));
    }
}