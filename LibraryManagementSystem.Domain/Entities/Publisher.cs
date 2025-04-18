namespace LibraryManagementSystem.Domain.Entities
{
    public class Publisher
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public DateOnly FoundedDate { get; private set; }
        public int NumberOfBooksPublished { get; private set; }


        //## ******************** RELATIONS ******************** ##//
        public ICollection<Book> Books { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
