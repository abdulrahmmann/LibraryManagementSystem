using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Handlers;

public class GetPublisherByPhoneQueryHandler: IRequestHandler<GetPublisherByPhoneQuery, BaseResponse<PublisherDto>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPublisherByPhoneQueryHandler> _logger;
    #endregion
    
    #region INJECT INSTANCES INTO CONSTRUCTOR
    public GetPublisherByPhoneQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
        ILogger<GetPublisherByPhoneQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion

    
    public async Task<BaseResponse<PublisherDto>> Handle(GetPublisherByPhoneQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PhoneNumber is null)
            {
                _logger.LogWarning($"request PhoneNumber: {request.PhoneNumber} can not be null!!");
                return BaseResponse<PublisherDto>.NoContentResponse($"request PhoneNumber: {request.PhoneNumber} can not be null!!");
            }
            var publisher = await _unitOfWork.PublisherRepository.GetPublisherByPhoneNumberAsync(request.PhoneNumber);

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
            
            _logger.LogInformation("Successfully Retrieving Publisher By PhoneNumber: {PhoneNumber}", request.PhoneNumber);

            return BaseResponse<PublisherDto>.SuccessResponse(publisherDto, $"Successfully Retrieving Publisher By PhoneNumber: {request.PhoneNumber}");
        }
        catch (Exception e)
        {
            _logger.LogError("An unexpected error occurred: {Message}.", e.Message);
            return BaseResponse<PublisherDto>.InternalServerErrorResponse("An unexpected error occurred.");
        }
    }
}