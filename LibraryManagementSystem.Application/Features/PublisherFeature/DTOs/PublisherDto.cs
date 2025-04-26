namespace LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;

public sealed class PublisherDto
{
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public DateOnly FoundedDate { get; set; }

    public int NumberOfBooksPublished { get; set; } = 0;
}