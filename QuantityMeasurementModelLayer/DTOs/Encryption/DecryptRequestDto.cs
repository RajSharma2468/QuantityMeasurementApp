namespace QuantityMeasurementModelLayer.DTOs.Encryption;

public class DecryptRequestDto
{
    public string CipherText { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
}