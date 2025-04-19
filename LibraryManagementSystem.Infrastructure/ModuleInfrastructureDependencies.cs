using LibraryManagementSystem.Domain.IGenericRepository;
using LibraryManagementSystem.Domain.IRepository;
using LibraryManagementSystem.Infrastructure.Context;
using LibraryManagementSystem.Infrastructure.GenericRepository;
using LibraryManagementSystem.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection service)
        {
            // REGISTER DAPPER CONTEXT
            service.AddSingleton<DapperContext>();

            // REGISTER GENERIC REPOSITORY
            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // REGISTER REPOSITORIES
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<IAuthorRepository, AuthorRepository>();
            service.AddScoped<IPublisherRepository, PublisherRepository>();
            service.AddScoped<IGenreRepository, GenreRepository>();
            service.AddScoped<IBookReviewRepository, BookReviewRepository>();
            service.AddScoped<IUserRepository, UserRepository>();


            return service;
        }
    }
}
