using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Handlers;

public class GetPublisherByNameQueryHandler: IRequestHandler<GetPublisherByNameQuery, BaseResponse<PublisherDto>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPublisherByNameQueryHandler> _logger;
    #endregion
    
    #region INJECT INSTANCES INTO CONSTRUCTOR
    public GetPublisherByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
        ILogger<GetPublisherByNameQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<PublisherDto>> Handle(GetPublisherByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Name is null)
            {
                _logger.LogWarning($"request Name: {request.Name} can not be null!!");
                return BaseResponse<PublisherDto>.NoContentResponse($"request Name: {request.Name} can not be null!!");
            }
            var publisher = await _unitOfWork.PublisherRepository.GetPublisherByNameAsync(request.Name);

            if (publisher is null)
            {
                _logger.LogWarning("publisher Is Null or Empty!!!");
                return BaseResponse<PublisherDto>.NoContentResponse("publisher Is Null or Empty!!!");
            }
            
            var publisherDto = _mapper.Map<PublisherDto>(publisher);

            if (publisherDto is null)
            {
                _logger.LogWarning("Publisher DTO Is Null or Empty!!!");
                return BaseResponse<PublisherDto>.NoContentResponse("Publisher DTO Is Null or Empty!!!");
            }
            
            _logger.LogInformation("Successfully Retrieving Publisher By Name: {Name}", request.Name);

            return BaseResponse<PublisherDto>.SuccessResponse(publisherDto, $"Successfully Retrieving Publisher By Name: {request.Name}");
        }
        catch (Exception e)
        {
            _logger.LogError("An unexpected error occurred: {Message}.", e.Message);
            return BaseResponse<PublisherDto>.InternalServerErrorResponse("An unexpected error occurred.");
        }
    }
}