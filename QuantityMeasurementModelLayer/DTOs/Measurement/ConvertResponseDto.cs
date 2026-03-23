namespace QuantityMeasurementModelLayer.DTOs.Measurement
{
    public class ConvertResponseDto
    {
        public double InputValue { get; set; }
        public string FromUnit { get; set; } = string.Empty;
        public double OutputValue { get; set; }
        public string ToUnit { get; set; } = string.Empty;
        public string Formula { get; set; } = string.Empty;
    }
}