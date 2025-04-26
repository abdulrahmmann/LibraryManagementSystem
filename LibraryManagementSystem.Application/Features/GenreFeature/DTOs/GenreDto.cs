namespace LibraryManagementSystem.Application.Features.GenreFeature.DTOs;

public sealed class GenreDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double AverageRating { get; set; }
}