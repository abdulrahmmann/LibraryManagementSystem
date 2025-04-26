using LibraryManagementSystem.Application.Features.GenreFeature.DTOs;
using LibraryManagementSystem.Domain.Entities;
using Mapster;

namespace LibraryManagementSystem.Application.Mapping;

public static class GenreMapping
{
    public static void Configure()
    {
        TypeAdapterConfig<Genre, GenreDto>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AverageRating, src => src.AverageRating);
        
        TypeAdapterConfig<GenreDto, Genre>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.AverageRating, src => src.AverageRating);
    }
}