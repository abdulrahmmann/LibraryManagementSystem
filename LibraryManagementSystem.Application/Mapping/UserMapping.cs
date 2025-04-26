using LibraryManagementSystem.Application.Features.UserFeature.DTOs;
using LibraryManagementSystem.Domain.Entities;
using Mapster;

namespace LibraryManagementSystem.Application.Mapping
{
    public static class UserMapping
    {
        public static void Configure()
        {
            // LOGIN
            TypeAdapterConfig<User, LoginUserDto>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.PasswordHash);

            TypeAdapterConfig<LoginUserDto, User>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PasswordHash, src => src.Password);

            // REGISTER
            TypeAdapterConfig<User, RegisterUserDto>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.PasswordHash)
                .Map(dest => dest.UserName, src => src.UserName);

            TypeAdapterConfig<RegisterUserDto, User>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PasswordHash, src => src.Password)
                .Map(dest => dest.UserName, src => src.UserName);
        }
    }
}
