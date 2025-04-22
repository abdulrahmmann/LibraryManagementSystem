using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        #region Field Instance
        private readonly ApplicationContext _dbContext;
        private readonly DapperContext _dapperContext;
        #endregion


        #region Inject Instances Into Constructor
        public GenreRepository(ApplicationContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dapperContext = dapperContext;
        }
        #endregion


        public async Task<Genre> GetGenreByNameAsync(string Name)
        {
            return await _dbContext.Genre.SingleOrDefaultAsync(g => g.Name == Name);
        }
        public async Task AddGenreAsync(Genre Genre)
        {
            await _dbContext.Genre.AddAsync(Genre);
        }
        public async Task AddRangeGenresAsync(IEnumerable<Genre> Genres)
        {
            await _dbContext.Genre.AddRangeAsync(Genres);
        }

        public void RemoveGenre(int Id)
        {
            _dbContext.Genre.Remove(_dbContext.Genre.Find(Id));
        }
        
        public bool IsExist(string Name)
        {
            var publisher = _dbContext.Publisher.AsQueryable().Where(a => a.Name.Equals(Name));

            return publisher.Any() ? true : false;
        }
    }
}
