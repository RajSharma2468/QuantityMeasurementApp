namespace QuantityMeasurementModelLayer.DTOs.Measurement
{
    public class ConvertRequestDto
    {
        public string Category { get; set; } = string.Empty; // Length, Weight, Temperature
        public string FromUnit { get; set; } = string.Empty;
        public string ToUnit { get; set; } = string.Empty;
        public double Value { get; set; }
    }
}