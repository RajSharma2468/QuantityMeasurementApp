namespace QuantityMeasurementModelLayer.DTOs.Encryption;

public class EncryptRequestDto
{
    public string PlainText { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
}