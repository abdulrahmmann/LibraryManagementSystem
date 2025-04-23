namespace LibraryManagementSystem.Application.Features.BookFeature.DTOs
{
    public sealed class BookDTO
    {
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public DateOnly PublishedDate { get; set; }

        public int NumberOfPages { get; set; }

        public int Edition { get; set; }

        public string ISPN { get; set; } = string.Empty;

        public string CoverColor { get; set; } = string.Empty;

        public string? BookCoverImage { get; set; } = string.Empty;
        
        public string AuthorName { get; set; } = string.Empty;
        
        public string GenreName { get; set; } = string.Empty;
        
        public string PublisherName { get; set; } = string.Empty;
    }
}