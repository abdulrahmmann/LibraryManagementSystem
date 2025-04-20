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
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, BaseResponse<bool>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAuthorCommandHandler> _logger;
        private readonly IValidator<AuthorDTO> _validator;
        #endregion


        #region INJECT INSTANCES INTO CONSTRUCTOR
        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<CreateAuthorCommandHandler> logger, IValidator<AuthorDTO> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }
        #endregion


        public async Task<BaseResponse<bool>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.AuthorDTO == null)
                {
                    _logger.LogWarning("AuthorDTO is null");

                    return BaseResponse<bool>.ErrorResponse("Request AuthorDTO cannot be null!");
                }

                var isExist = _unitOfWork.AuthorRepository.IsExist(request.AuthorDTO.Name);

                if (isExist)
                {
                    _logger.LogWarning("Author with Name: {Name} already exists", request.AuthorDTO.Name);

                    return BaseResponse<bool>.ConflictResponse($"Author with Name: {request.AuthorDTO.Name} Is Already Exist!!");
                }

                _logger.LogInformation("Handling CreateAuthor with Name: {Name}", request.AuthorDTO.Name);

                var validationResult = await _validator.ValidateAsync(request.AuthorDTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));

                    _logger.LogWarning("Validation failed: {Errors}", errors);

                    return BaseResponse<bool>.ErrorResponse(errors);
                }

                var author = _mapper.Map<Author>(request.AuthorDTO);

                await _unitOfWork.AuthorRepository.AddAuthorAsync(author);

                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Author created with Name: {Name}", author.Name);

                return BaseResponse<bool>.CreatedResponse(true, $"Author with name `{author.Name}` created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an author");

                return BaseResponse<bool>.ErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
