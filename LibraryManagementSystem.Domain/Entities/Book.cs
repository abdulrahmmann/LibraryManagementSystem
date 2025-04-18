namespace LibraryManagementSystem.Domain.Entities
{
    public class Book
    {
        public int Id { get; private set; }

        public string Title { get; private set; } = string.Empty;

        public string Summary { get; private set; } = string.Empty;

        public DateOnly PublishedDate { get; private set; }

        public int NumberOfPages { get; private set; }

        public int Edition { get; private set; }

        public string ISPN { get; private set; } = string.Empty;

        public string CoverColor { get; private set; } = string.Empty;

        public string? BookCoverImage { get; private set; } = string.Empty;

        //******************** RELATIONS ********************//
        public int AuthorId { get; private set; }
        public Author Author { get; private set; } = new();

        public int GenreId { get; private set; }
        public Genre Genre { get; private set; } = new();

        public ICollection<Copy> BookCopies { get; private set; } = [];

        public ICollection<Review> BookReviews { get; private set; } = [];

        public int PublisherId { get; private set; }
        public Publisher Publisher { get; private set; } = new();

        //******************** RELATIONS ********************//
    }
}
