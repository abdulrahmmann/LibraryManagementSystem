using LibraryManagementSystem.Domain.IGenericRepository;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;

namespace LibraryManagementSystem.Application.UOF
{
    public class UnitOfWork : IUnitOfWork
    {
        #region INSTANCE FILEDS
        private readonly ApplicationContext _dbContext;
        private readonly Dictionary<Type, Object> _repositories;
        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }
        public IUserRepository UserRepository { get; }
        public IPublisherRepository PublisherRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IBookReviewRepository BookReviewRepository { get; }
        #endregion

        #region INJECT INSTANCES INTO CONSTRUCTORS
        public UnitOfWork(ApplicationContext dbContext, IAuthorRepository authorRepository, IBookRepository bookRepository, IUserRepository userRepository,
            IPublisherRepository publisherRepository, IGenreRepository genericRepository, IBookReviewRepository bookReviewRepository )
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
            UserRepository = userRepository;
            PublisherRepository = publisherRepository;
            GenreRepository = genericRepository;
            BookReviewRepository = bookReviewRepository;
        }
        #endregion
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IGenericRepository<T>)_repositories[typeof(T)];
            }

            var repository = new GenericRepository<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
