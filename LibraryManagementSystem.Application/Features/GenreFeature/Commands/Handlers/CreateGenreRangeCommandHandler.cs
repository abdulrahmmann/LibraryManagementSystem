using FluentValidation;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.GenreFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.GenreFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using LibraryManagementSystem.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.GenreFeature.Commands.Handlers;

public class CreateGenreRangeCommandHandler: IRequestHandler<CreateGenreRangeCommand, BaseResponse<bool>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateGenreRangeCommandHandler> _logger;
    private readonly IValidator<GenreDto> _validator;
    #endregion

    #region INJECT INSTANCES INTO CONSTRUCTOR
    public CreateGenreRangeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<CreateGenreRangeCommandHandler> logger, IValidator<GenreDto> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _validator = validator;
    }
    #endregion
    
    
    public async Task<BaseResponse<bool>> Handle(CreateGenreRangeCommand request, CancellationToken cancellationToken)
    {
             try
        {
            if (request?.GenreDtos is null || !request.GenreDtos.Any())
            {
                _logger.LogWarning("AuthorDTOs Request cannot be null or empty.");

                return BaseResponse<bool>.ErrorResponse("AuthorDTOs Request cannot be null or empty.");
            }

            foreach (var genre in request.GenreDtos)
            {
                var isExist = _unitOfWork.GenreRepository.IsExist(genre.Name);

                if (isExist)
                {
                    _logger.LogWarning("Genre with Name: {Name} already exists", genre.Name);

                    return BaseResponse<bool>.ConflictResponse($"Genre with Name: {genre.Name} Is Already Exist!!");
                }
            }

            var validationErrors = new List<string>();
            var validGenre = new List<Genre>();

            foreach (var dto in request.GenreDtos)
            {
                var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    validationErrors.AddRange(validationResult.Errors.Select(e => $"Name: {dto.Name ?? "N/A"} - {e.ErrorMessage}"));
                    continue;
                }
                
                validGenre.Add(_mapper.Map<Genre>(dto));
            }

            if (validationErrors.Any())
            {
                var errors = string.Join("; ", validationErrors);

                _logger.LogWarning("Validation failed: {Errors}", errors);

                return BaseResponse<bool>.ValidationErrorResponse(errors);
            }

            await _unitOfWork.GenreRepository.AddRangeGenresAsync(validGenre);

            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("{Count} Genres created successfully.", validGenre.Count);

            return BaseResponse<bool>.CreatedResponse(true, $"{validGenre.Count} Genres created successfully.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating Genres.");

            return BaseResponse<bool>.InternalServerErrorResponse("An unexpected error occurred.");
        }
    }
}