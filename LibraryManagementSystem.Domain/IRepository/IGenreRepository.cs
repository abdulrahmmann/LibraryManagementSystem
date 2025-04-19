using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IGenericRepository;

namespace LibraryManagementSystem.Domain.IRepository
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        #region GET METHODS
        public Task<Genre> GetGenreByNameAsync(string Name);
        #endregion


        #region POST METHODS
        public Task AddGenreAsync(Genre Genre);
        public Task AddRangeGenresAsync(IEnumerable<Genre> Genres);
        #endregion

        #region DELETE METHODS
        public void RemoveGenre(int Id);
        #endregion


        #region PUT METHODS

        #endregion
    }
}
