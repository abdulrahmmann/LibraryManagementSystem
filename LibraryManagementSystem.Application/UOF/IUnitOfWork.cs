using LibraryManagementSystem.Domain.IGenericRepository;
using LibraryManagementSystem.Domain.IRepository;

namespace LibraryManagementSystem.Application.UOF
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;

        IAuthorRepository AuthorRepository { get; }
        IBookRepository BookRepository { get; }
        IUserRepository UserRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        IGenreRepository GenreRepository { get; }
        IBookReviewRepository BookReviewRepository { get; }

        Task SaveChangesAsync();

        void SaveChanges();
    }
}
