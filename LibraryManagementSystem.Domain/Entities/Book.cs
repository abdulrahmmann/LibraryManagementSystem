namespace LibraryManagementSystem.Domain.Entities
{
    public class Book
    {
        public int Id { get; private set; }

        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public DateOnly PublishedDate { get; set; }

        public int NumberOfPages { get; set; }

        public int Edition { get; set; }

        public string Isbn { get; set; } = string.Empty;

        public string CoverColor { get; set; } = string.Empty;

        public string? BookCoverImage { get; set; } = string.Empty;

        //******************** RELATIONS ********************//
        public int AuthorId { get; set; }
        public Author Author { get; set; } = new();

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = new();

        public ICollection<Copy> BookCopies { get; set; } = [];

        public ICollection<Review> BookReviews { get; set; } = [];

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = new();

        public ICollection<BorrowRequest> BorrowRequest { get; set; } = [];

        //******************** RELATIONS ********************//
    }
}
