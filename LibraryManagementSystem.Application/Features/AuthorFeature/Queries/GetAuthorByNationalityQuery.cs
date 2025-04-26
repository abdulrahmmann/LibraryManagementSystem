using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public record GetAuthorByNationalityQuery(string Nationality) : IRequest<BaseResponse<IEnumerable<AuthorDto>>>;
}