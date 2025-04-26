﻿using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Handlers;

public class GetAllPublisherQueryHandler: IRequestHandler<GetAllPublisherQuery, BaseResponse<IEnumerable<PublisherDto>>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllPublisherQueryHandler> _logger;
    #endregion
    
    #region INJECT INSTANCES INTO CONSTRUCTOR
    public GetAllPublisherQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
        ILogger<GetAllPublisherQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    #endregion


    public async Task<BaseResponse<IEnumerable<PublisherDto>>> Handle(GetAllPublisherQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var publishers = await _unitOfWork.PublisherRepository.GetAllData();

            if (!publishers.Any() )
            {
                _logger.LogWarning("publishers is null or empty!.");
                return BaseResponse<IEnumerable<PublisherDto>>.NoContentResponse("publishers is null or empty!.");
            }
            
            var publisherDTOs = _mapper.Map<IEnumerable<PublisherDto>>(publishers);

            if (!publisherDTOs.Any())
            {
                _logger.LogWarning("publishers Map is null or empty!.");
                return BaseResponse<IEnumerable<PublisherDto>>.NoContentResponse("publishers Map is null or empty!.");
            }
            
            return BaseResponse<IEnumerable<PublisherDto>>.SuccessResponse(publisherDTOs);
        }
        catch (Exception e)
        {
            _logger.LogError("An unexpected error occurred: {e.Message}.", e.Message);
            return BaseResponse<IEnumerable<PublisherDto>>.InternalServerErrorResponse($"An unexpected error occurred: {e.Message}.");
        }
    }
}