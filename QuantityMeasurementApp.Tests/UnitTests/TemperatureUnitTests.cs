using Xunit;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests.UnitTests;

public class TemperatureUnitTests
{
    [Fact]
    public void TemperatureUnit_Symbols_AreCorrect()
    {
        Assert.Equal("°C", TemperatureUnit.Celsius.GetUnitSymbol());
        Assert.Equal("°F", TemperatureUnit.Fahrenheit.GetUnitSymbol());
        Assert.Equal("K", TemperatureUnit.Kelvin.GetUnitSymbol());
    }

    [Fact]
    public void TemperatureUnit_Names_AreCorrect()
    {
        Assert.Equal("Celsius", TemperatureUnit.Celsius.GetUnitName());
        Assert.Equal("Fahrenheit", TemperatureUnit.Fahrenheit.GetUnitName());
        Assert.Equal("Kelvin", TemperatureUnit.Kelvin.GetUnitName());
    }

    [Fact]
    public void TemperatureUnit_ConvertToBaseUnit_Celsius_ReturnsSame()
    {
        var result = TemperatureUnit.Celsius.ConvertToBaseUnit(25);
        Assert.Equal(25, result);
    }

    [Fact]
    public void TemperatureUnit_ConvertToBaseUnit_Fahrenheit_ConvertsCorrectly()
    {
        var result = TemperatureUnit.Fahrenheit.ConvertToBaseUnit(32);
        Assert.Equal(0, result, 5);
    }

    [Fact]
    public void TemperatureUnit_ConvertToBaseUnit_Kelvin_ConvertsCorrectly()
    {
        var result = TemperatureUnit.Kelvin.ConvertToBaseUnit(273.15);
        Assert.Equal(0, result, 5);
    }

    [Fact]
    public void TemperatureUnit_ConvertFromBaseUnit_Celsius_ReturnsSame()
    {
        var result = TemperatureUnit.Celsius.ConvertFromBaseUnit(25);
        Assert.Equal(25, result);
    }

    [Fact]
    public void TemperatureUnit_ConvertFromBaseUnit_Fahrenheit_ConvertsCorrectly()
    {
        var result = TemperatureUnit.Fahrenheit.ConvertFromBaseUnit(0);
        Assert.Equal(32, result, 5);
    }

    [Fact]
    public void TemperatureUnit_ConvertFromBaseUnit_Kelvin_ConvertsCorrectly()
    {
        var result = TemperatureUnit.Kelvin.ConvertFromBaseUnit(0);
        Assert.Equal(273.15, result, 5);
    }

    [Fact]
    public void TemperatureUnit_ValidateOperationSupport_ThrowsForAnyOperation()
    {
        Assert.Throws<UnsupportedOperationException>(() => 
            TemperatureUnit.Celsius.ValidateOperationSupport("ADD"));
        Assert.Throws<UnsupportedOperationException>(() => 
            TemperatureUnit.Celsius.ValidateOperationSupport("SUBTRACT"));
        Assert.Throws<UnsupportedOperationException>(() => 
            TemperatureUnit.Celsius.ValidateOperationSupport("DIVIDE"));
    }

    [Fact]
    public void TemperatureUnit_Equals_Works()
    {
        Assert.True(TemperatureUnit.Celsius.Equals(TemperatureUnit.Celsius));
        Assert.False(TemperatureUnit.Celsius.Equals(TemperatureUnit.Fahrenheit));
    }

    [Fact]
    public void TemperatureUnit_StaticConvert_CelsiusToFahrenheit_Works()
    {
        var result = TemperatureUnit.Convert(0, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit);
        Assert.Equal(32, result, 5);
    }

    [Fact]
    public void TemperatureUnit_StaticConvert_FahrenheitToCelsius_Works()
    {
        var result = TemperatureUnit.Convert(32, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius);
        Assert.Equal(0, result, 5);
    }

    [Fact]
    public void TemperatureUnit_StaticConvert_CelsiusToKelvin_Works()
    {
        var result = TemperatureUnit.Convert(0, TemperatureUnit.Celsius, TemperatureUnit.Kelvin);
        Assert.Equal(273.15, result, 5);
    }

    [Fact]
    public void TemperatureUnit_StaticConvert_SameUnit_ReturnsSame()
    {
        var result = TemperatureUnit.Convert(100, TemperatureUnit.Celsius, TemperatureUnit.Celsius);
        Assert.Equal(100, result);
    }
}