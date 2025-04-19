using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region Field Instance
        private readonly ApplicationContext _dbContext;
        private readonly DapperContext _dapperContext;
        #endregion


        #region Inject Instances Into Constructor
        public UserRepository(ApplicationContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dapperContext = dapperContext;
        }
        #endregion

        public async Task<IEnumerable<User>> GetUserByCountryAsync(string Country)
        {
            return await _dbContext.Users
                .Where(a => a.Country.ToLower() == Country.ToLower())
                .ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(a => a.Email == Email);
        }

        public async Task<IEnumerable<User>> GetUserByNameAsync(string Name)
        {
            return await _dbContext.Users
                .Where(a => a.UserName!.ToLower().Contains(Name.ToLower()))
                .ToListAsync();
        }

        public async Task<User> GetUserByPhoneNumberAsync(string PhoneNumber)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(a => a.PhoneNumber == PhoneNumber);
        }

        public async Task AddRangeUsersAsync(IEnumerable<User> Users)
        {
            await _dbContext.Users.AddRangeAsync(Users);
        }

        public async Task AddUserAsync(User User)
        {
            await _dbContext.Users.AddAsync(User);
        }

        public void RemoveUser(int Id)
        {
            _dbContext.Users.Remove(_dbContext.Users.Find(Id));
        }
    }
}
