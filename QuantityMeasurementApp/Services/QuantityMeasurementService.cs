using System;
using System.Collections.Generic;
using System.Linq;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Core.Services;

public class QuantityMeasurementService
{
    private readonly List<QuantityLength> _lengthMeasurements = new();
    private readonly List<QuantityWeight> _weightMeasurements = new();

    public void AddLengthMeasurement(QuantityLength measurement)
    {
        if (measurement is null)
            throw new ArgumentNullException(nameof(measurement));
        
        _lengthMeasurements.Add(measurement);
    }

    public void AddWeightMeasurement(QuantityWeight measurement)
    {
        if (measurement is null)
            throw new ArgumentNullException(nameof(measurement));
        
        _weightMeasurements.Add(measurement);
    }

    public bool CompareLengthMeasurements(QuantityLength first, QuantityLength second)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Equals(second);
    }

    public bool CompareWeightMeasurements(QuantityWeight first, QuantityWeight second)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Equals(second);
    }

    public QuantityLength ConvertLength(QuantityLength source, LengthUnit targetUnit)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        
        return source.ConvertTo(targetUnit);
    }

    public QuantityWeight ConvertWeight(QuantityWeight source, WeightUnit targetUnit)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        
        return source.ConvertTo(targetUnit);
    }

    public QuantityLength AddLengthMeasurements(QuantityLength first, QuantityLength second)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Add(second);
    }

    public QuantityWeight AddWeightMeasurements(QuantityWeight first, QuantityWeight second)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Add(second);
    }

    public QuantityLength AddLengthMeasurementsWithTarget(QuantityLength first, QuantityLength second, LengthUnit targetUnit)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Add(second, targetUnit);
    }

    public QuantityWeight AddWeightMeasurementsWithTarget(QuantityWeight first, QuantityWeight second, WeightUnit targetUnit)
    {
        if (first is null || second is null)
            throw new ArgumentNullException(first is null ? nameof(first) : nameof(second));
        
        return first.Add(second, targetUnit);
    }

    public IReadOnlyList<QuantityLength> GetAllLengthMeasurements() => _lengthMeasurements.AsReadOnly();
    public IReadOnlyList<QuantityWeight> GetAllWeightMeasurements() => _weightMeasurements.AsReadOnly();
    
    public void ClearAllMeasurements()
    {
        _lengthMeasurements.Clear();
        _weightMeasurements.Clear();
    }
}