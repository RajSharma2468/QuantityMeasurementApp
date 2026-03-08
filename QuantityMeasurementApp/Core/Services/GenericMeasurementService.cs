using System.Collections.ObjectModel;
using QuantityMeasurementApp.Core.Abstractions;
using QuantityMeasurementApp.Core.Domain.Quantities;

namespace QuantityMeasurementApp.Core.Services;

public class GenericMeasurementService
{
    private readonly List<object> _measurements = new();

    public void AddMeasurement<T>(GenericQuantity<T> measurement) where T : IMeasurable
    {
        if (measurement == null)
            throw new ArgumentNullException(nameof(measurement));
        
        _measurements.Add(measurement);
    }

    public bool CompareMeasurements<T>(GenericQuantity<T> first, GenericQuantity<T> second) where T : IMeasurable
    {
        if (first == null || second == null)
            throw new ArgumentNullException(first == null ? nameof(first) : nameof(second));
        
        return first.Equals(second);
    }

    public GenericQuantity<T> ConvertMeasurement<T>(GenericQuantity<T> source, T targetUnit) where T : IMeasurable
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));
        
        return source.ConvertTo(targetUnit);
    }

    public GenericQuantity<T> AddMeasurements<T>(GenericQuantity<T> first, GenericQuantity<T> second) where T : IMeasurable
    {
        if (first == null || second == null)
            throw new ArgumentNullException(first == null ? nameof(first) : nameof(second));
        
        return first.Add(second);
    }

    public GenericQuantity<T> AddMeasurementsWithTarget<T>(GenericQuantity<T> first, GenericQuantity<T> second, T targetUnit) where T : IMeasurable
    {
        if (first == null || second == null)
            throw new ArgumentNullException(first == null ? nameof(first) : nameof(second));
        if (targetUnit == null)
            throw new ArgumentNullException(nameof(targetUnit));
        
        return first.Add(second, targetUnit);
    }

    public ReadOnlyCollection<object> GetAllMeasurements() => _measurements.AsReadOnly();
    
    public List<GenericQuantity<T>> GetMeasurementsByType<T>() where T : IMeasurable
    {
        return _measurements.OfType<GenericQuantity<T>>().ToList();
    }
    
    public void ClearAllMeasurements()
    {
        _measurements.Clear();
    }
}