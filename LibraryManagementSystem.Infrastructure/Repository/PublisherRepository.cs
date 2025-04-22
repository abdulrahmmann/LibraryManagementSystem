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

        public IQueryable<Publisher> FilterPublishersByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return _dbContext.Publisher;
            }

            return _dbContext.Publisher.Where(p => EF.Functions.Like(p.Name,$"%{Name}%"));
        }

        public IQueryable<Publisher> FilterPublishersByEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return _dbContext.Publisher;
            }
            return _dbContext.Publisher.Where(p => EF.Functions.Like(p.Email, $"%{Email}%"));
        }

        public async Task<Publisher> GetPublisherByEmailAsync(string Email)
        {
            return await _dbContext.Publisher.SingleOrDefaultAsync(p => p.Email.Equals(Email));
        }

        public async Task<Publisher> GetPublisherByNameAsync(string Name)
        {
            return await _dbContext.Publisher.SingleOrDefaultAsync(p => p.Name.Equals(Name));
        }

        public async Task<Publisher> GetPublisherByPhoneNumberAsync(string PhoneNumber)
        {
            return await _dbContext.Publisher.SingleOrDefaultAsync(p => p.PhoneNumber.Equals(PhoneNumber));
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
        
        public bool IsExist(string Name)
        {
            var publisher = _dbContext.Publisher.AsQueryable().Where(a => a.Name.Equals(Name));

            return publisher.Any() ? true : false;
        }
    }
}
