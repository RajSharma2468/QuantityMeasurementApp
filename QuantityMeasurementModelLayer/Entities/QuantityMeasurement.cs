using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementModelLayer.Entities;

public class QuantityMeasurement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required]
    public double ThisValue { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string ThisUnit { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string ThisMeasurementType { get; set; } = string.Empty;
    
    [Required]
    public double ThatValue { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string ThatUnit { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string ThatMeasurementType { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string Operation { get; set; } = string.Empty;
    
    public string? ResultString { get; set; }
    
    public double ResultValue { get; set; }
    
    [MaxLength(50)]
    public string? ResultUnit { get; set; }
    
    [MaxLength(50)]
    public string? ResultMeasurementType { get; set; }
    
    public string? ErrorMessage { get; set; }
    
    public bool IsError { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    // Add this property if you want soft delete
    public bool IsDeleted { get; set; }
}