namespace LibraryManagementSystem.Application.Features.AuthorFeature.DTOs
{
    public class AuthorDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Biography { get; set; } = string.Empty;

        public int NumberOfBooks { get; set; }

        public string Nationality { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }
    }
}
