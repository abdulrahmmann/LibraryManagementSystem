using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Commands
{
    public record CreateAuthorCommand(AuthorDto AuthorDTO) : IRequest<BaseResponse<bool>>;
}