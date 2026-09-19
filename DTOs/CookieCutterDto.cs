namespace AkkWebService.DTOs;

public class CookieCutterDto
{
    public long CookieCutterId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? LengthInches { get; set; }
    public decimal? WidthInches { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Comments { get; set; }
    public DateOnly? DateAcquired { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
