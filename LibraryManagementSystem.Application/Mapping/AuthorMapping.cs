using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Domain.Entities;
using Mapster;

namespace LibraryManagementSystem.Application.Mapping
{
    public static class AuthorMapping
    {
        public static void Configure()
        {
            TypeAdapterConfig<Author, AuthorDTO>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Biography, src => src.Biography)
                .Map(dest => dest.Nationality, src => src.Nationality)
                .Map(dest => dest.BirthDate, src => src.BirthDate)
                .Map(dest => dest.NumberOfBooks, src => src.NumberOfBooks);

            TypeAdapterConfig<AuthorDTO, Author>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Biography, src => src.Biography)
                .Map(dest => dest.Nationality, src => src.Nationality)
                .Map(dest => dest.BirthDate, src => src.BirthDate)
                .Map(dest => dest.NumberOfBooks, src => src.NumberOfBooks);
        }
    }
}
