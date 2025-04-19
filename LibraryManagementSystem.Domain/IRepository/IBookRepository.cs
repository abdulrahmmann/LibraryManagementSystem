using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IGenericRepository;

namespace LibraryManagementSystem.Domain.IRepository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        #region GET METHODS
        public Task<Book> GetBookByTitle(string Title);
        public Task<Book> GetBookByEdition(int Edition);
        public Task<Book> GetBookByIspn(string ISPN);
        public Task<Book> GetBookByPublishedDate(string PublishedDate);
        #endregion


        #region POST METHODS
        public Task AddBookAsync(Book Book);
        public Task AddBookRangeAsync(IEnumerable<Book> Books);
        #endregion


        #region DELETE METHODS

        #endregion


        #region PUT METHODS

        #endregion
    }
}
