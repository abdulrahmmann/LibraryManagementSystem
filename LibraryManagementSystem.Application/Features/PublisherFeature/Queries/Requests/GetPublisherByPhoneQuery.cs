using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;

public sealed record GetPublisherByPhoneQuery(string PhoneNumber): IRequest<BaseResponse<PublisherDto>>;