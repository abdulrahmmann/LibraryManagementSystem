using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repository
{
    internal class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        #region
        private readonly ApplicationContext _dbContext;
        private readonly DapperContext _dapperContext;
        #endregion

        #region
        public PublisherRepository(ApplicationContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dapperContext = dapperContext;
        }
        #endregion

        public async Task<Publisher> GetPublisherByEmailAsync(string Email)
        {
            return await _dbContext.Publisher.SingleOrDefaultAsync(p => p.Email == Email);
        }

        public async Task<Publisher> GetPublisherByNameAsync(string Name)
        {
            return await _dbContext.Publisher.SingleOrDefaultAsync(p => p.Name == Name);
        }

        public async Task<Publisher> GetPublisherByPhoneNumberAsync(string PhoneNumber)
        {
            return await _dbContext.Publisher.SingleOrDefaultAsync(p => p.PhoneNumber == PhoneNumber);
        }
        public async Task AddPublisherAsync(Publisher Publisher)
        {
            await _dbContext.Publisher.AddAsync(Publisher);
        }

        public async Task AddRangePublishersAsync(IEnumerable<Publisher> Publishers)
        {
            await _dbContext.Publisher.AddRangeAsync(Publishers);
        }

        public void RemovePublisher(int Id)
        {
            _dbContext.Publisher.Remove(_dbContext.Publisher.Find(Id));
        }
    }
}
