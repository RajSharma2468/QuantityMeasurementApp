namespace QuantityMeasurementModelLayer.Entities;

public class EncryptionHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string InputText { get; set; } = string.Empty;
    public string OutputText { get; set; } = string.Empty;
    public string EncryptionType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}