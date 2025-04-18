namespace LibraryManagementSystem.Domain.Entities
{
    public class Review
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public string Comment { get; private set; } = string.Empty;
        public DateOnly ReviewDate { get; private set; }
        public int Rating { get; private set; }  // Rating out of 5


        //## ******************** RELATIONS ******************** ##//

        public int BookId { get; private set; }
        public Book Book { get; private set; } = new();

        //## ******************** RELATIONS ******************** ##//
    }
}
