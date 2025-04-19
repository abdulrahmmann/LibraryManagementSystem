using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IGenericRepository;

namespace LibraryManagementSystem.Domain.IRepository
{
    public interface IBookReviewRepository : IGenericRepository<Review>
    {
        #region GET METHODS
        public Task<Review> GetReviewByNameAsync(string Name);
        public Task<Review> GetReviewByTitleAsync(string Title);
        #endregion


        #region POST METHODS
        public Task AddReviewAsync(Review Review);
        public Task AddRangeReviewsAsync(IEnumerable<Review> Reviews);
        #endregion


        #region DELETE METHODS
        public void RemoveBookReview(int Id);
        #endregion


        #region PUT METHODS

        #endregion
    }
}
