using FluentValidation;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.GenreFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.GenreFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using LibraryManagementSystem.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.GenreFeature.Commands.handlers;

public class CreateGenreCommandHandler: IRequestHandler<CreateGenreCommand, BaseResponse<bool>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateGenreCommandHandler> _logger;
    private readonly IValidator<GenreDto> _validator;
    #endregion

    #region INJECT INSTANCES INTO CONSTRUCTOR
    public CreateGenreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<CreateGenreCommandHandler> logger, IValidator<GenreDto> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
    }
    #endregion
    
    
    public async Task<BaseResponse<bool>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.GenreDto is null)
            {
                _logger.LogWarning("request can not be null!");

                return BaseResponse<bool>.ErrorResponse("request can not be null!");
            }

            var isExist = _unitOfWork.PublisherRepository.IsExist(request.GenreDto.Name);

            if (isExist)
            {
                _logger.LogWarning("Genre with Name: {Name} already exists", request.GenreDto.Name);

                return BaseResponse<bool>.ConflictResponse($"Genre with Name: {request.GenreDto.Name} Is Already Exist!!");
            }

            var validationResult = await _validator.ValidateAsync(request.GenreDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var error = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage));
                
                _logger.LogError("Validation Error: {Error}", error);
                return BaseResponse<bool>.ValidationErrorResponse(error);
            }

            var genre = _mapper.Map<Genre>(request.GenreDto);

            await _unitOfWork.GenreRepository.AddGenreAsync(genre);

            await _unitOfWork.SaveChangesAsync();
            
            _logger.LogInformation("Genre created with Name: {Name}", genre.Name);

            return BaseResponse<bool>.CreatedResponse(true, $"Genre with name `{genre.Name}` created successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating an Genre");

            return BaseResponse<bool>.ErrorResponse("An unexpected error occurred.");
        }
    }
}