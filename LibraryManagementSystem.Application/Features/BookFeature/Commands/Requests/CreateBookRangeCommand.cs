using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.BookFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookFeature.Commands.Requests;

public record CreateBookRangeCommand(IEnumerable<CreateBookDto> BookDto): IRequest<BaseResponse<bool>>;