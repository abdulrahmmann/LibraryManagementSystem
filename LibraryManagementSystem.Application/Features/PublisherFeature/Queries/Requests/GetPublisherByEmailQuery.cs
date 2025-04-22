using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;

public sealed record GetPublisherByEmailQuery(string Email): IRequest<BaseResponse<PublisherDTO>>;