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
        private readonly DapperContext _dapperContext;
        #endregion


        #region Inject Instances Into Constructor
        public BookRepository(ApplicationContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dapperContext = dapperContext;
        }
        #endregion


        public async Task<Book> GetBookByEdition(int Edition)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.Edition == Edition);
        }

        public async Task<Book> GetBookByIspn(string ISPN)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.ISPN == ISPN);
        }

        public async Task<Book> GetBookByPublishedDate(string PublishedDate)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.PublishedDate.ToString() == PublishedDate);
        }

        public async Task<Book> GetBookByTitle(string Title)
        {
            return await _dbContext.Book.SingleOrDefaultAsync(x => x.Title == Title);
        }

        public async Task AddBookAsync(Book Book)
        {
            await _dbContext.Book.AddAsync(Book);
        }

        public async Task AddBookRangeAsync(IEnumerable<Book> Books)
        {
            await _dbContext.Book.AddRangeAsync(Books);
        }
    }
}
