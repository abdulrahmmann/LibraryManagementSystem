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

public class CreatePublisherCommandHandler: IRequestHandler<CreatePublisherCommand, BaseResponse<bool>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePublisherCommandHandler> _logger;
    private readonly IValidator<PublisherDTO> _validator;
    #endregion

    #region INJECT INSTANCES INTO CONSTRUCTOR
    public CreatePublisherCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<CreatePublisherCommandHandler> logger, IValidator<PublisherDTO> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
    }
    #endregion

    public async Task<BaseResponse<bool>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.PublisherDto is null)
            {
                _logger.LogWarning("request can not be null!");

                return BaseResponse<bool>.ErrorResponse("request can not be null!");
            }

            var isExist = _unitOfWork.PublisherRepository.IsExist(request.PublisherDto.Name);

            if (isExist)
            {
                _logger.LogWarning("Publisher with Name: {Name} already exists", request.PublisherDto.Name);

                return BaseResponse<bool>.ConflictResponse($"Publisher with Name: {request.PublisherDto.Name} Is Already Exist!!");
            }

            var validationResult = await _validator.ValidateAsync(request.PublisherDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var error = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage));
                
                _logger.LogError("Validation Error: {Error}", error);
                return BaseResponse<bool>.ValidationErrorResponse(error);
            }

            var publisher = _mapper.Map<Publisher>(request.PublisherDto);

            await _unitOfWork.PublisherRepository.AddPublisherAsync(publisher);

            await _unitOfWork.SaveChangesAsync();
            
            _logger.LogInformation("Publisher created with Name: {Name}", publisher.Name);

            return BaseResponse<bool>.CreatedResponse(true, $"Publisher with name `{publisher.Name}` created successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating a publisher");

            return BaseResponse<bool>.ErrorResponse("An unexpected error occurred.");
        }
    }
}