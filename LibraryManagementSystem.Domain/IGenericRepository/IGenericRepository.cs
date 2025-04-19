namespace LibraryManagementSystem.Domain.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllData();

        Task<T> GetById(int Id);

        Task Add(T entity);

        void Delete(int Id);

        Task SaveChangesAsync();
    }
}
