using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Commands.Requests;

public sealed record CreatePublishersRangeCommand(IEnumerable<PublisherDTO> PublisherDtos): IRequest<BaseResponse<bool>>;