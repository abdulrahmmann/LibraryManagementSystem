using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public record FilterAuthorByNameQuery(string searchName) : IRequest<BaseResponse<IQueryable<AuthorDTO>>>;
}
