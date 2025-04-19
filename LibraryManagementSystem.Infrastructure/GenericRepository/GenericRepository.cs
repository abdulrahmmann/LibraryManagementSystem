using LibraryManagementSystem.Domain.IGenericRepository;
using LibraryManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fields Instances
        private readonly ApplicationContext _dbContext;
        private readonly DbSet<T> _dbSet;
        #endregion


        #region Inject Instances Into Constructor
        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        #endregion


        public async Task<IEnumerable<T>> GetAllData()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(int Id)
        {
            var entity = _dbSet.Find(Id);
            _dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}