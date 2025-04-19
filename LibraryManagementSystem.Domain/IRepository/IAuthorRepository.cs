using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IGenericRepository;

namespace LibraryManagementSystem.Domain.IRepository
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        #region GET METHODS
        public Task<IEnumerable<Author>> GetAuthorsByNameAsync(string Name);
        public Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(string Nationality);
        #endregion


        #region POST METHODS
        public Task AddAuthorAsync(Author Author);
        public Task AddRangeAuthorsAsync(IEnumerable<Author> Authors);
        #endregion


        #region DELETE METHODS
        public void RemoveAuthor(int Id);
        #endregion


        #region PUT METHODS

        #endregion
    }
}
