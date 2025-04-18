namespace LibraryManagementSystem.Domain.Entities
{
    public class Copy
    {
        public int Id { get; private set; }
        public DateTime AddedDate { get; private set; }


        //## ******************** RELATIONS ******************** ##//
        public int BookId { get; set; }
        public Book Book { get; set; } = new();

        //## BOOKCOPY  - BORROWREQUEST -> A borrow request is linked to a single book copy ##//
        public ICollection<BorrowRequest> BorrowRequests { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
