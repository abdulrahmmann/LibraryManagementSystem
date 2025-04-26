using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Handlers;

public class FilterPublisherByEmailQueryHandler: IRequestHandler<FilterPublisherByEmailQuery, BaseResponse<IQueryable<PublisherDto>>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<FilterPublisherByEmailQueryHandler> _logger;
    #endregion

    #region CONSTRUCTOR
    public FilterPublisherByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<FilterPublisherByEmailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion

    public Task<BaseResponse<IQueryable<PublisherDto>>> Handle(FilterPublisherByEmailQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var publisherQuery = _unitOfWork.PublisherRepository.FilterPublishersByName(request.SearchEmail);

            var publisherList = publisherQuery.ToList();

            if (!publisherList.Any())
            {
                _logger.LogWarning("No publishers found matching the email: {SearchName}", request.SearchEmail);

                return Task.FromResult(BaseResponse<IQueryable<PublisherDto>>.NoContentResponse("No publishers found."));
            }

            var publishersDto = _mapper.Map<List<PublisherDto>>(publisherList);

            if (!publishersDto.Any())
            {
                _logger.LogWarning("Mapping to PublishersDto resulted in an empty list for email: {SearchEmail}", request.SearchEmail);

                return Task.FromResult(BaseResponse<IQueryable<PublisherDto>>.NoContentResponse("No publishers found after mapping."));
            }

            return Task.FromResult(BaseResponse<IQueryable<PublisherDto>>.SuccessResponse(publishersDto.AsQueryable(), "Successfully retrieved publishers."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving publishers for email: {SearchEmail}", request.SearchEmail);

            return Task.FromResult(BaseResponse<IQueryable<PublisherDto>>.InternalServerErrorResponse("An unexpected error occurred."));
        }
    }
}