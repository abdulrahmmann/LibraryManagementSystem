using FluentValidation;
using LibraryManagementSystem.Application.Features.AuthorFeature.Validators;
using LibraryManagementSystem.Application.Mapping;
using LibraryManagementSystem.Application.UOF;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using LibraryManagementSystem.Application.Features.BookFeature.Validators;
using LibraryManagementSystem.Application.Features.GenreFeature.Validators;
using LibraryManagementSystem.Application.Features.PublisherFeature.Validators;

namespace LibraryManagementSystem.Application
{
    public static class ModuleAppDependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection service)
        {
            // Register MediatR
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register Mapster
            service.AddMapster();
            AuthorMapping.Configure();
            UserMapping.Configure();
            PublisherMapping.Configure();
            GenreMapping.Configure();

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(AppDomain.CurrentDomain.GetAssemblies());

            // Register FluentValidation
            // If you have multiple Validators in your Application layer
            service.AddValidatorsFromAssembly(typeof(AuthorValidator).Assembly);
            service.AddValidatorsFromAssembly(typeof(PublisherValidator).Assembly);
            service.AddValidatorsFromAssembly(typeof(GenreValidator).Assembly);
            service.AddValidatorsFromAssembly(typeof(CreateBookValidator).Assembly);


            // REGISTER UNIT OF WORK
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            return service;
        }
    }
}
//ChangeBookIsbnSpelling