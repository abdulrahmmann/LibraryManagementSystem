namespace LibraryManagementSystem.Application.Features.BookFeature.DTOs
{
    public sealed class CreateBookDto
    {
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public DateOnly PublishedDate { get; set; }

        public int NumberOfPages { get; set; }

        public int Edition { get; set; }

        public string Isbn { get; set; } = string.Empty;

        public string CoverColor { get; set; } = string.Empty;

        public string? BookCoverImage { get; set; } = string.Empty;
        
        /********** If The Author|Genre|Publisher Doesn't Exist -> Create new Author|Genre|Publisher. **********/
        // Author
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorBiography { get; set; } = string.Empty;
        public int AuthorNumberOfBooks { get; set; }
        public string AuthorNationality { get; set; } = string.Empty;
        public DateOnly AuthorBirthDate { get; set; }
        
        // Genre
        public string GenreName { get; set; } = string.Empty;
        public string GenreDescription { get; set; } = string.Empty;
        public double GenreAverageRating { get; set; }
        
        // Publisher
        public string PublisherName { get; set; } = string.Empty;
        public string PublisherEmail { get; set; } = string.Empty;
        public string PublisherPhoneNumber { get; set; } = string.Empty;
        public DateOnly PublisherFoundedDate { get; set; }
        /*******************************************************************************************************/
    }
}