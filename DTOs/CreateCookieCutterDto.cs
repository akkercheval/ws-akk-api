using System.ComponentModel.DataAnnotations;

namespace AkkWebService.DTOs;

public class CreateCookieCutterDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public decimal? LengthInches { get; set; }

    public decimal? WidthInches { get; set; }

    [Required]
    [MaxLength(30)]
    public string Type { get; set; } = string.Empty;

    public string? Comments { get; set; }

    public DateOnly? DateAcquired { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }
}
