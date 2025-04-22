using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Domain.Entities;
using Mapster;

namespace LibraryManagementSystem.Application.Mapping;

public static class PublisherMapping
{
    public static void Configure()
    {
        TypeAdapterConfig<Publisher, PublisherDTO>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.FoundedDate, src => src.FoundedDate)
            .Map(dest => dest.NumberOfBooksPublished, src => src.NumberOfBooksPublished);

        TypeAdapterConfig<PublisherDTO, Publisher>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
            .Map(dest => dest.FoundedDate, src => src.FoundedDate)
            .Map(dest => dest.NumberOfBooksPublished, src => src.NumberOfBooksPublished);
    }
}