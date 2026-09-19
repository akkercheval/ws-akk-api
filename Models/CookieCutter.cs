using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkkWebService.Models;

[Table("CookieCutters")]
public class CookieCutter
{
    [Key]
    public long CookieCutterId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(6,2)")]
    public decimal? LengthInches { get; set; }

    [Column(TypeName = "decimal(6,2)")]
    public decimal? WidthInches { get; set; }

    [Required]
    [MaxLength(30)]
    public string Type { get; set; } = string.Empty;

    public string? Comments { get; set; }

    public DateOnly? DateAcquired { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [MaxLength(100)]
    public string? CreatedBy { get; set; }

    [MaxLength(100)]
    public string? UpdatedBy { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
}
