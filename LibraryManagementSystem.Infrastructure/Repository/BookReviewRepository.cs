using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repository
{
    public class BookReviewRepository : GenericRepository<Review>, IBookReviewRepository
    {
        #region Field Instance
        private readonly ApplicationContext _dbContext;
        private readonly DapperContext _dapperContext;
        #endregion


        #region Inject Instances Into Constructor
        public BookReviewRepository(ApplicationContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dapperContext = dapperContext;
        }
        #endregion

        public async Task<Review> GetReviewByNameAsync(string Name)
        {
            return await _dbContext.BookReview.SingleOrDefaultAsync(r => r.Name == Name);
        }

        public async Task<Review> GetReviewByTitleAsync(string Title)
        {
            return await _dbContext.BookReview.SingleOrDefaultAsync(r => r.Title == Title);
        }

        public async Task AddReviewAsync(Review Review)
        {
            await _dbContext.BookReview.AddAsync(Review);
        }

        public async Task AddRangeReviewsAsync(IEnumerable<Review> Reviews)
        {
            await _dbContext.BookReview.AddRangeAsync(Reviews);
        }

        public void RemoveBookReview(int Id)
        {
            _dbContext.BookReview.Remove(_dbContext.BookReview.Find(Id));
        }
    }
}
