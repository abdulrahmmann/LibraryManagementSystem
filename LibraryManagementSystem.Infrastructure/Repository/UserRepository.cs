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
        #endregion


        #region Inject Instances Into Constructor
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        public async Task<IEnumerable<User>> GetUserByCountryAsync(string Country)
        {
            return await _dbContext.Users
                .Where(a => a.Country.Trim().ToLower() == Country.Trim().ToLower())
                .ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(a => a.Email == Email);
        }

        public async Task<IEnumerable<User>> GetUserByNameAsync(string Name)
        {
            return await _dbContext.Users
                .Where(a => a.UserName!.Trim().ToLower().Contains(Name.Trim().ToLower()))
                .ToListAsync();
        }
        public IQueryable<User> FilterUserByName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return _dbContext.Users;
            }

            return _dbContext.Users.Where(u => EF.Functions.Like(u.UserName, $"%{Name}%"));
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

        public bool IsExist(string UserName, string Email)
        {
            var authors = _dbContext.Users.AsQueryable()
                .Where(u => u.UserName!.Equals(UserName) && u.Email!.Equals(Email));

            return authors.Any() ? true : false;
        }
    }
}
