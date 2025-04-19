using FluentValidation;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using LibraryManagementSystem.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Commands
{
    public class CreateAuthorRangeCommandHandler : IRequestHandler<CreateAuthorRangeCommand, BaseResponse<bool>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAuthorRangeCommandHandler> _logger;
        private readonly IValidator<AuthorDTO> _validator;
        #endregion


        #region INJECT INSTANCES INTO CONSTRUCTOR
        public CreateAuthorRangeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<CreateAuthorRangeCommandHandler> logger, IValidator<AuthorDTO> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }
        #endregion

        public async Task<BaseResponse<bool>> Handle(CreateAuthorRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AuthorDTOs == null || !request.AuthorDTOs.Any())
                {
                    _logger.LogWarning("AuthorDTOs is null or empty.");

                    return BaseResponse<bool>.ErrorResponse("AuthorDTOs cannot be null or empty.");
                }

                _logger.LogInformation("Handling creation of multiple authors.");

                var validationErrors = new List<string>();

                var validAuthors = new List<Author>();

                foreach (var dto in request.AuthorDTOs)
                {
                    var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        validationErrors.AddRange(validationResult.Errors.Select(e => $"Name: {dto.Name ?? "N/A"} - {e.ErrorMessage}"));
                        continue;
                    }

                    validAuthors.Add(_mapper.Map<Author>(dto));
                }

                if (validationErrors.Any())
                {
                    var errors = string.Join("; ", validationErrors);

                    _logger.LogWarning("Validation failed: {Errors}", errors);

                    return BaseResponse<bool>.ValidationErrorResponse(errors);
                }

                await _unitOfWork.AuthorRepository.AddRangeAuthorsAsync(validAuthors);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("{Count} authors created successfully.", validAuthors.Count);

                return BaseResponse<bool>.CreatedResponse(true, $"{validAuthors.Count} authors created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating authors.");

                return BaseResponse<bool>.InternalServerErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
