using Microsoft.AspNetCore.Identity;
namespace LibraryManagementSystem.Domain.Entities
{
    public class User : IdentityUser
    {
        public DateOnly JoinedDate { get; private set; }

        public string Country { get; private set; } = string.Empty;

        public DateOnly BirthDate { get; private set; }

        public string? UserImage { get; private set; } = string.Empty;


        //## ******************** RELATIONS ******************** ##//
        public ICollection<BorrowRequest> BorrowRequests { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
