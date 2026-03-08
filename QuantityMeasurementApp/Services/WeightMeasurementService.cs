using System;
using System.Collections.Generic;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Core.Services;

public class WeightMeasurementService
{
    private readonly List<QuantityWeight> _measurements = new();

    public void AddMeasurement(QuantityWeight measurement)
    {
        if (measurement is null)
            throw new ArgumentNullException(nameof(measurement));
        
        _measurements.Add(measurement);
    }

    public bool CompareMeasurements(QuantityWeight first, QuantityWeight second)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Equals(second);
    }

    public QuantityWeight ConvertMeasurement(QuantityWeight source, WeightUnit targetUnit)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        
        return source.ConvertTo(targetUnit);
    }

    public QuantityWeight AddMeasurements(QuantityWeight first, QuantityWeight second)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Add(second);
    }

    public QuantityWeight AddMeasurementsWithTarget(QuantityWeight first, QuantityWeight second, WeightUnit targetUnit)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Add(second, targetUnit);
    }

    public bool ValidateWeightInput(double value, WeightUnit unit)
    {
        return !double.IsNaN(value) && 
               !double.IsInfinity(value) && 
               Enum.IsDefined(typeof(WeightUnit), unit);
    }

    public IReadOnlyList<QuantityWeight> GetAllMeasurements() => _measurements.AsReadOnly();
    public void ClearMeasurements() => _measurements.Clear();
}