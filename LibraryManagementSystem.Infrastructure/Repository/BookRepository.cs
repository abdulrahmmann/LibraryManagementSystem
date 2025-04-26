using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        #region Field Instance
        private readonly ApplicationContext _dbContext;
        #endregion


        #region Inject Instances Into Constructor
        public BookRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion


        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _dbContext.Book.ToListAsync();
        }
        
        public async Task<Book> GetBookByEdition(int Edition)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.Edition == Edition);
        }

        public async Task<Book> GetBookByIsbn(string Isbn)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.Isbn.Equals(Isbn));
        }

        public async Task<Book> GetBookByPublishedDate(string PublishedDate)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.PublishedDate.ToString().Equals(PublishedDate));
        }

        public async Task<Book> GetBookByTitle(string Title)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.Title.Equals(Title));
        }

        public async Task AddBookAsync(Book Book)
        {
            await _dbContext.Book.AddAsync(Book);
        }

        public async Task AddBookRangeAsync(IEnumerable<Book> Books)
        {
            await _dbContext.Book.AddRangeAsync(Books);
        }

        public bool IsExistingBook(string Title)
        {
            var isExisting = _dbContext.Book.Any(b => b.Title.Equals(Title));
            
            return isExisting? true : false;
        }
    }
}
