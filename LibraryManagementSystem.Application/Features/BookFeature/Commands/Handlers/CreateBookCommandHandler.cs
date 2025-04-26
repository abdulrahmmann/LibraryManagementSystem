using FluentValidation;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.BookFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.BookFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using LibraryManagementSystem.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.BookFeature.Commands.Handlers
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BaseResponse<bool>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookCommandHandler> _logger;
        private readonly IValidator<CreateBookDto> _validator;
        #endregion

        #region INJECT INSTANCES INTO CONSTRUCTOR
        public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<CreateBookCommandHandler> logger, IValidator<CreateBookDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }
        #endregion

        public async Task<BaseResponse<bool>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.CreateBookDto is null)
                {
                    _logger.LogWarning("Request cannot be null!");
                    return BaseResponse<bool>.ErrorResponse("Request cannot be null!");
                }

                // Check if book already exists
                var isExist = _unitOfWork.BookRepository.IsExistingBook(request.CreateBookDto.Title);
                if (isExist)
                {
                    _logger.LogWarning("Book with Title: {Title} already exists", request.CreateBookDto.Title);
                    return BaseResponse<bool>.ConflictResponse($"Book with Title: {request.CreateBookDto.Title} is already exist.");
                }

                // Get Author, Genre, and Publisher (Async)
                var author = await _unitOfWork.AuthorRepository.GetAuthorByNameAsync(request.CreateBookDto.AuthorName);
                var genre = await _unitOfWork.GenreRepository.GetGenreByNameAsync(request.CreateBookDto.GenreName);
                var publisher = await _unitOfWork.PublisherRepository.GetPublisherByNameAsync(request.CreateBookDto.PublisherName);

                // Validate CreateBookDto
                var validationResult = await _validator.ValidateAsync(request.CreateBookDto, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var error = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage));
                    _logger.LogError("Validation Error: {Error}", error);
                    return BaseResponse<bool>.ValidationErrorResponse(error);
                }

                // Create new Book
                var newBook = new Book
                {
                    Title = request.CreateBookDto.Title,
                    Summary = request.CreateBookDto.Summary,
                    PublishedDate = request.CreateBookDto.PublishedDate,
                    NumberOfPages = request.CreateBookDto.NumberOfPages,
                    Edition = request.CreateBookDto.Edition,
                    Isbn = request.CreateBookDto.Isbn,
                    CoverColor = request.CreateBookDto.CoverColor,
                    BookCoverImage = request.CreateBookDto.BookCoverImage,
                    AuthorId = author.Id,  // Use the Author ID
                    GenreId = genre.Id,    // Use the Genre ID
                    PublisherId = publisher.Id  // Use the Publisher ID
                };

                await _unitOfWork.BookRepository.AddBookAsync(newBook);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Book created with Title: {Title}", request.CreateBookDto.Title);

                return BaseResponse<bool>.CreatedResponse(true, $"Book with Title `{request.CreateBookDto.Title}` created successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while creating a Book: {e.Message}", e.Message);
                return BaseResponse<bool>.ErrorResponse($"An unexpected error occurred: {e.Message}");
            }
        }
    }
}
