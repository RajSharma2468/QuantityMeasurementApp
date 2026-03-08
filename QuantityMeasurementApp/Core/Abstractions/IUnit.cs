namespace QuantityMeasurementApp.Core.Abstractions
{
    public interface IUnit
    {
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);
    }
}