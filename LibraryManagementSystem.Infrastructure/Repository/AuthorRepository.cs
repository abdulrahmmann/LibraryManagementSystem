using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repository
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        #region
        private readonly ApplicationContext _dbContext;
        private readonly DapperContext _dapperContext;
        #endregion

        #region
        public AuthorRepository(ApplicationContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dapperContext = dapperContext;
        }
        #endregion

        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string Name)
        {
            return await _dbContext.Author
                .Where(a => a.Name.Contains(Name, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(string Nationality)
        {
            return await _dbContext.Author
                .Where(a => a.Nationality.Equals(Nationality, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
        public async Task AddAuthorAsync(Author Author)
        {
            await _dbContext.Author.AddAsync(Author);
        }

        public async Task AddRangeAuthorsAsync(IEnumerable<Author> Authors)
        {
            await _dbContext.Author.AddRangeAsync(Authors);
        }

        public void RemoveAuthor(int Id)
        {
            _dbContext.Author.Remove(_dbContext.Author.Find(Id));
        }
    }
}
