using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.IGenericRepository;

namespace LibraryManagementSystem.Domain.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        #region GET METHODS
        public Task<IEnumerable<User>> GetUserByNameAsync(string Name);
        public Task<User> GetUserByEmailAsync(string Email);
        public Task<User> GetUserByPhoneNumberAsync(string PhoneNumber);
        public Task<IEnumerable<User>> GetUserByCountryAsync(string Country);

        public IQueryable<User> FilterUserByName(string Name);
        #endregion


        #region POST METHODS
        public Task AddUserAsync(User User);
        public Task AddRangeUsersAsync(IEnumerable<User> Users);
        #endregion


        #region DELETE METHODS
        public void RemoveUser(int Id);
        #endregion


        #region PUT METHODS

        #endregion

        public bool IsExist(string UserName, string Email);
    }
}
