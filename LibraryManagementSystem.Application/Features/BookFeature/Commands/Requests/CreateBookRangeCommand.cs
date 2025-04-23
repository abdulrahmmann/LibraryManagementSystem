using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.BookFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookFeature.Commands.Requests;

public record CreateBookRangeCommand(IEnumerable<BookDTO> BookDto): IRequest<BaseResponse<bool>>;