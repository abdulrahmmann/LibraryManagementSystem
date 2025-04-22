using FluentValidation;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.PublisherFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using LibraryManagementSystem.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Commands.handlers;

public class CreatePublishersRangeCommandHandler: IRequestHandler<CreatePublishersRangeCommand, BaseResponse<bool>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePublishersRangeCommandHandler> _logger;
    private readonly IValidator<PublisherDTO> _validator;
    #endregion

    #region INJECT INSTANCES INTO CONSTRUCTOR
    public CreatePublishersRangeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<CreatePublishersRangeCommandHandler> logger, IValidator<PublisherDTO> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<bool>> Handle(CreatePublishersRangeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request?.PublisherDtos is null || !request.PublisherDtos.Any())
            {
                _logger.LogWarning("AuthorDTOs Request cannot be null or empty.");

                return BaseResponse<bool>.ErrorResponse("AuthorDTOs Request cannot be null or empty.");
            }

            foreach (var publisher in request.PublisherDtos)
            {
                var isExist = _unitOfWork.PublisherRepository.IsExist(publisher.Name);

                if (isExist)
                {
                    _logger.LogWarning("Publisher with Name: {Name} already exists", publisher.Name);

                    return BaseResponse<bool>.ConflictResponse($"Publisher with Name: {publisher.Name} Is Already Exist!!");
                }
            }

            var validationErrors = new List<string>();
            var validPublisher = new List<Publisher>();

            foreach (var dto in request.PublisherDtos)
            {
                var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    validationErrors.AddRange(validationResult.Errors.Select(e => $"Name: {dto.Name ?? "N/A"} - {e.ErrorMessage}"));
                    continue;
                }
                
                validPublisher.Add(_mapper.Map<Publisher>(dto));
            }

            if (validationErrors.Any())
            {
                var errors = string.Join("; ", validationErrors);

                _logger.LogWarning("Validation failed: {Errors}", errors);

                return BaseResponse<bool>.ValidationErrorResponse(errors);
            }

            await _unitOfWork.PublisherRepository.AddRangePublishersAsync(validPublisher);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("{Count} Publishers created successfully.", validPublisher.Count);

            return BaseResponse<bool>.CreatedResponse(true, $"{validPublisher.Count} Publishers created successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating publishers.");

            return BaseResponse<bool>.InternalServerErrorResponse("An unexpected error occurred.");
        }
    }
}