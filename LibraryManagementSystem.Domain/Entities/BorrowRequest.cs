namespace LibraryManagementSystem.Domain.Entities
{
    public class BorrowRequest
    {
        public int Id { get; private set; }

        public DateOnly BorrowedDate { get; private set; }

        public DateOnly ReturnDate { get; private set; }

        public DateOnly DueDate { get; private set; }

        //## ******************** RELATIONS ******************** ##//
        public int UserId { get; private set; }
        public User User { get; private set; } = new();

        public int BookId { get; private set; }
        public Book Book { get; private set; } = new();

        /*public int BookCopyId { get; private set; }
        public Copy Copy { get; private set; } = new();*/

        //## ******************** RELATIONS ******************** ##//
    }
}
