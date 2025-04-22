using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Handlers;

public class GetPublisherByEmailQueryHandler : IRequestHandler<GetPublisherByEmailQuery, BaseResponse<PublisherDTO>>
{
    #region INSTANCE FIELDS

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPublisherByEmailQueryHandler> _logger;

    #endregion

    #region INJECT INSTANCES INTO CONSTRUCTOR

    public GetPublisherByEmailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<GetPublisherByEmailQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    public async Task<BaseResponse<PublisherDTO>> Handle(GetPublisherByEmailQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (request.Email is null)
            {
                _logger.LogWarning($"request Email: {request.Email} can not be null!!");
                return BaseResponse<PublisherDTO>.NoContentResponse(
                    $"request Email: {request.Email} can not be null!!");
            }

            var publisher = await _unitOfWork.PublisherRepository.GetPublisherByEmailAsync(request.Email);

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

            _logger.LogInformation("Successfully Retrieving Publisher By Email: {Email}", request.Email);

            return BaseResponse<PublisherDTO>.SuccessResponse(publisherDto,
                $"Successfully Retrieving Publisher By Email: {request.Email}");
        }
        catch (Exception e)
        {
            _logger.LogError("An unexpected error occurred: {Message}.", e.Message);
            return BaseResponse<PublisherDTO>.InternalServerErrorResponse("An unexpected error occurred.");
        }
    }
}