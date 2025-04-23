using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Domain.Entities
{
    public class Author
    {
        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public string Biography { get; set; } = string.Empty;

        public int NumberOfBooks { get; set; }

        public string Nationality { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }


        //## ******************** RELATIONS ******************** ##//
        public ICollection<Book> Books { get; set; } = [];

        //## ******************** RELATIONS ******************** ##//
    }
}
