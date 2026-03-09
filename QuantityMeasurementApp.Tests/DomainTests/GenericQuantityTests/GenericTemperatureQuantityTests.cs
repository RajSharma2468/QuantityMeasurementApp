using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;
using System;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests;

public class GenericTemperatureQuantityTests
{
    private const double Epsilon = 1e-10;

    #region Equality Tests

    [Fact]
    public void CelsiusToCelsius_SameValue_AreEqual()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(0, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(0, TemperatureUnit.Celsius);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void FahrenheitToFahrenheit_SameValue_AreEqual()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit);
        var t2 = new GenericQuantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void KelvinToKelvin_SameValue_AreEqual()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin);
        var t2 = new GenericQuantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void CelsiusToFahrenheit_0CelsiusEquals32Fahrenheit()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(0, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void CelsiusToFahrenheit_100CelsiusEquals212Fahrenheit()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(212, TemperatureUnit.Fahrenheit);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void CelsiusToFahrenheit_Negative40Equal()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(-40, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(-40, TemperatureUnit.Fahrenheit);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void CelsiusToKelvin_0CelsiusEquals27315Kelvin()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(0, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void FahrenheitToKelvin_32FahrenheitEquals27315Kelvin()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit);
        var t2 = new GenericQuantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin);
        
        Assert.True(t1.Equals(t2));
    }

    [Fact]
    public void TemperatureVsLength_AreNotEqual()
    {
        var temp = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var length = new GenericQuantity<LengthUnit>(100, LengthUnit.Feet);
        
        Assert.False(temp.Equals(length));
    }

    [Fact]
    public void TemperatureVsWeight_AreNotEqual()
    {
        var temp = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        var weight = new GenericQuantity<WeightUnit>(50, WeightUnit.Kilogram);
        
        Assert.False(temp.Equals(weight));
    }

    [Fact]
    public void TemperatureVsVolume_AreNotEqual()
    {
        var temp = new GenericQuantity<TemperatureUnit>(25, TemperatureUnit.Celsius);
        var volume = new GenericQuantity<VolumeUnit>(25, VolumeUnit.Litre);
        
        Assert.False(temp.Equals(volume));
    }

    #endregion

    #region Conversion Tests

    [Fact]
    public void Convert_CelsiusToFahrenheit_0CTo32F()
    {
        var temp = new GenericQuantity<TemperatureUnit>(0, TemperatureUnit.Celsius);
        var result = temp.ConvertTo(TemperatureUnit.Fahrenheit);
        
        Assert.Equal(32, result.Value, 5);
        Assert.Equal(TemperatureUnit.Fahrenheit.GetUnitSymbol(), result.Unit.GetUnitSymbol());
    }

    [Fact]
    public void Convert_CelsiusToFahrenheit_100CTo212F()
    {
        var temp = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var result = temp.ConvertTo(TemperatureUnit.Fahrenheit);
        
        Assert.Equal(212, result.Value, 5);
    }

    [Fact]
    public void Convert_CelsiusToFahrenheit_Negative40CToNegative40F()
    {
        var temp = new GenericQuantity<TemperatureUnit>(-40, TemperatureUnit.Celsius);
        var result = temp.ConvertTo(TemperatureUnit.Fahrenheit);
        
        Assert.Equal(-40, result.Value, 5);
    }

    [Fact]
    public void Convert_FahrenheitToCelsius_32FTo0C()
    {
        var temp = new GenericQuantity<TemperatureUnit>(32, TemperatureUnit.Fahrenheit);
        var result = temp.ConvertTo(TemperatureUnit.Celsius);
        
        Assert.Equal(0, result.Value, 5);
    }

    [Fact]
    public void Convert_FahrenheitToCelsius_212FTo100C()
    {
        var temp = new GenericQuantity<TemperatureUnit>(212, TemperatureUnit.Fahrenheit);
        var result = temp.ConvertTo(TemperatureUnit.Celsius);
        
        Assert.Equal(100, result.Value, 5);
    }

    [Fact]
    public void Convert_CelsiusToKelvin_0CTo27315K()
    {
        var temp = new GenericQuantity<TemperatureUnit>(0, TemperatureUnit.Celsius);
        var result = temp.ConvertTo(TemperatureUnit.Kelvin);
        
        Assert.Equal(273.15, result.Value, 5);
    }

    [Fact]
    public void Convert_KelvinToCelsius_27315KTo0C()
    {
        var temp = new GenericQuantity<TemperatureUnit>(273.15, TemperatureUnit.Kelvin);
        var result = temp.ConvertTo(TemperatureUnit.Celsius);
        
        Assert.Equal(0, result.Value, 5);
    }

    [Fact]
    public void Convert_RoundTrip_CelsiusToFahrenheitToCelsius()
    {
        var original = new GenericQuantity<TemperatureUnit>(25, TemperatureUnit.Celsius);
        var toF = original.ConvertTo(TemperatureUnit.Fahrenheit);
        var backToC = toF.ConvertTo(TemperatureUnit.Celsius);
        
        Assert.Equal(original.Value, backToC.Value, 5);
    }

    #endregion

    #region Unsupported Operation Tests

    [Fact]
    public void Add_Temperature_ThrowsUnsupportedOperationException()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => t1.Add(t2));
    }

    [Fact]
    public void Subtract_Temperature_ThrowsUnsupportedOperationException()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => t1.Subtract(t2));
    }

    [Fact]
    public void Divide_Temperature_ThrowsUnsupportedOperationException()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => t1.Divide(t2));
    }

    [Fact]
    public void Add_TemperatureWithTarget_ThrowsUnsupportedOperationException()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => t1.Add(t2, TemperatureUnit.Fahrenheit));
    }

    [Fact]
    public void Subtract_TemperatureWithTarget_ThrowsUnsupportedOperationException()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        Assert.Throws<UnsupportedOperationException>(() => t1.Subtract(t2, TemperatureUnit.Fahrenheit));
    }

    [Fact]
    public void Temperature_ErrorMessage_ContainsOperationInfo()
    {
        var t1 = new GenericQuantity<TemperatureUnit>(100, TemperatureUnit.Celsius);
        var t2 = new GenericQuantity<TemperatureUnit>(50, TemperatureUnit.Celsius);
        
        var ex = Assert.Throws<UnsupportedOperationException>(() => t1.Add(t2));
        Assert.Contains("ADD", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void AbsoluteZero_CelsiusToKelvin()
    {
        var absoluteZeroC = new GenericQuantity<TemperatureUnit>(-273.15, TemperatureUnit.Celsius);
        var inKelvin = absoluteZeroC.ConvertTo(TemperatureUnit.Kelvin);
        
        Assert.Equal(0, inKelvin.Value, 5);
    }

    [Fact]
    public void AbsoluteZero_FahrenheitToKelvin()
    {
        var absoluteZeroF = new GenericQuantity<TemperatureUnit>(-459.67, TemperatureUnit.Fahrenheit);
        var inKelvin = absoluteZeroF.ConvertTo(TemperatureUnit.Kelvin);
        
        Assert.Equal(0, inKelvin.Value, 5);
    }

    #endregion
}