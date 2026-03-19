namespace QuantityMeasurementBusinessLayer.DTOs;

public class QuantityDTO
{
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string MeasurementType { get; set; } = string.Empty;
}

public class QuantityInputDTO
{
    public QuantityDTO ThisQuantity { get; set; } = new();
    public QuantityDTO ThatQuantity { get; set; } = new();
}

// The class name is QuantityResultDTO (without "Measurement" in the name)
public class QuantityResultDTO
{
    public long Id { get; set; }
    public double ThisValue { get; set; }
    public string ThisUnit { get; set; } = string.Empty;
    public string ThisMeasurementType { get; set; } = string.Empty;
    public double ThatValue { get; set; }
    public string ThatUnit { get; set; } = string.Empty;
    public string ThatMeasurementType { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty;
    public string? ResultString { get; set; }
    public double ResultValue { get; set; }
    public string? ResultUnit { get; set; }
    public string? ResultMeasurementType { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsError { get; set; }
    public DateTime CreatedAt { get; set; }
}