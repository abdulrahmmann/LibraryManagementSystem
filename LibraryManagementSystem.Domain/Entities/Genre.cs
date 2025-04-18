namespace LibraryManagementSystem.Domain.Entities
{
    public class Genre
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public double AverageRating { get; private set; }

        //## ******************** RELATIONS ******************** ##//
        public ICollection<Book> Books { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
