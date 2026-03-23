namespace QuantityMeasurementModelLayer.Entities;

public class Measurement
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FromUnit { get; set; } = string.Empty;
    public string ToUnit { get; set; } = string.Empty;
    public double InputValue { get; set; }
    public double OutputValue { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation property
    public User? User { get; set; }
}