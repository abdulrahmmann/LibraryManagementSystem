using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Handlers;

public class GetPublisherByIdQueryHandler: IRequestHandler<GetPublisherByIdQuery, BaseResponse<PublisherDTO>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPublisherByIdQueryHandler> _logger;
    #endregion
    
    #region INJECT INSTANCES INTO CONSTRUCTOR
    public GetPublisherByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
        ILogger<GetPublisherByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion
    
    public async Task<BaseResponse<PublisherDTO>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id < 0)
            {
                _logger.LogWarning($"request Id: {request.Id} can not be less than 0!!");
                return BaseResponse<PublisherDTO>.NoContentResponse($"request Id: {request.Id} can not be less than 0!!");
            }
            var publisher = await _unitOfWork.PublisherRepository.GetById(request.Id);

            if (publisher is null)
            {
                _logger.LogWarning("publisher Is Null or Empty!!!");
                return BaseResponse<PublisherDTO>.NoContentResponse("publisher Is Null or Empty!!!");
            }
            
            var publisherDto = _mapper.Map<PublisherDTO>(publisher);

            if (publisherDto is null)
            {
                _logger.LogWarning("Publisher DTO Is Null or Empty!!!");
                return BaseResponse<PublisherDTO>.NoContentResponse("Publisher DTO Is Null or Empty!!!");
            }
            
            _logger.LogInformation("Successfully Retrieving Publisher By Id: {Id}", request.Id);

            return BaseResponse<PublisherDTO>.SuccessResponse(publisherDto, $"Successfully Retrieving Publisher By Id: {request.Id}");
        }
        catch (Exception e)
        {
            _logger.LogError("An unexpected error occurred.");
            return BaseResponse<PublisherDTO>.InternalServerErrorResponse("An unexpected error occurred.");
        }
    }
}