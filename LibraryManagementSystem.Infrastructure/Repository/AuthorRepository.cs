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
        #endregion

        #region
        public AuthorRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        public IQueryable<Author> FilterAuthorByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return _dbContext.Author;
            }

            return _dbContext.Author.Where(a => EF.Functions.Like(a.Name, $"%{Name}%"));
        }

        public async Task<Author> GetAuthorByNameAsync(string Name)
        {
            return await _dbContext.Author.SingleOrDefaultAsync(a => a.Name.ToLower() == Name.ToLower());
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNationalityAsync(string Nationality)
        {
            return await _dbContext.Author
                .Where(a => a.Nationality.Trim().ToLower() == Nationality.Trim().ToLower())
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

        public bool IsExist(string Name)
        {
            var authors = _dbContext.Author.AsQueryable().Where(a => a.Name.Equals(Name));

            return authors.Any() ? true : false;
        }
    }
}
