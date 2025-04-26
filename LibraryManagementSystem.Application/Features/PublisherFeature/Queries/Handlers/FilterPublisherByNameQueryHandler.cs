using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Handlers;

public class FilterPublisherByNameQueryHandler: IRequestHandler<FilterPublisherByNameQuery, BaseResponse<IQueryable<PublisherDto>>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<FilterPublisherByNameQueryHandler> _logger;
    #endregion

    #region CONSTRUCTOR
    public FilterPublisherByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<FilterPublisherByNameQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<IQueryable<PublisherDto>>> Handle(FilterPublisherByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var publisherQuery = _unitOfWork.PublisherRepository.FilterPublishersByName(request.SearchName);

            var publisherList = publisherQuery.ToList();

            if (!publisherList.Any())
            {
                _logger.LogWarning("No publishers found matching the name: {SearchName}", request.SearchName);

                return BaseResponse<IQueryable<PublisherDto>>.NoContentResponse("No publishers found.");
            }

            var publishersDto = _mapper.Map<List<PublisherDto>>(publisherList);

            if (!publishersDto.Any())
            {
                _logger.LogWarning("Mapping to PublishersDto resulted in an empty list for name: {SearchName}", request.SearchName);

                return BaseResponse<IQueryable<PublisherDto>>.NoContentResponse("No publishers found after mapping.");
            }

            return BaseResponse<IQueryable<PublisherDto>>.SuccessResponse(publishersDto.AsQueryable(), "Successfully retrieved publishers.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving publishers for name: {SearchName}", request.SearchName);

            return BaseResponse<IQueryable<PublisherDto>>.InternalServerErrorResponse("An unexpected error occurred.");
        }
    }
}