namespace LibraryManagementSystem.Domain.Entities
{
    public class Author
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Biography { get; private set; } = string.Empty;

        public int NumberOfBooks { get; private set; }

        public string Nationality { get; private set; } = string.Empty;

        public DateOnly BirthDate { get; private set; }


        //## ******************** RELATIONS ******************** ##//
        public ICollection<Book> Books { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
