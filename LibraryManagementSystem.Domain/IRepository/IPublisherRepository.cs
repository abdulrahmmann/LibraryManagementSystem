using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IGenericRepository;

namespace LibraryManagementSystem.Domain.IRepository
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        #region GET METHODS
        public Task<Publisher> GetPublisherByNameAsync(string Name);
        public Task<Publisher> GetPublisherByEmailAsync(string Email);
        public Task<Publisher> GetPublisherByPhoneNumberAsync(string PhoneNumber);
        #endregion


        #region POST METHODS
        public Task AddPublisherAsync(Publisher Publisher);
        public Task AddRangePublishersAsync(IEnumerable<Publisher> Publishers);
        #endregion


        #region DELETE METHODS
        public void RemovePublisher(int Id);
        #endregion


        #region PUT METHODS

        #endregion
    }
}
