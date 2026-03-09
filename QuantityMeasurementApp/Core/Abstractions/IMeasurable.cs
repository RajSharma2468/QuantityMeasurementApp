namespace QuantityMeasurementApp.Core.Abstractions;

public interface IMeasurable
{
    double GetConversionFactor();
    double ConvertToBaseUnit(double value);
    double ConvertFromBaseUnit(double baseValue);
    string GetUnitSymbol();
    string GetUnitName();
}