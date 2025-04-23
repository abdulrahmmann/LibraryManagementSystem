namespace LibraryManagementSystem.Domain.Entities
{
    public class Copy
    {
        public int Id { get; private set; }
        public DateTime AddedDate { get; private set; }


        //## ******************** RELATIONS ******************** ##//
        public int BookId { get; set; }
        public Book Book { get; set; }

        //public ICollection<BorrowRequest> BorrowRequests { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
