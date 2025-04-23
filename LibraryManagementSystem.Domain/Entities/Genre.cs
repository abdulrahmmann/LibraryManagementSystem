namespace LibraryManagementSystem.Domain.Entities
{
    public class Genre
    {
        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double AverageRating { get; set; }

        //## ******************** RELATIONS ******************** ##//
        public ICollection<Book> Books { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
