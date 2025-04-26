﻿using FluentValidation;
using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.BookFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.BookFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using LibraryManagementSystem.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.BookFeature.Commands.Handlers;

public class CreateBookCommandHandler: IRequestHandler<CreateBookCommand, BaseResponse<bool>>
{
    #region INSTANCE FIELDS
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBookCommandHandler> _logger;
    private readonly IValidator<CreateBookDto> _validator;
    #endregion

    #region INJECT INSTANCES INTO CONSTRUCTOR

    private protected CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
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
                _logger.LogWarning("request can not be null!");
                return BaseResponse<bool>.ErrorResponse("request can not be null!");
            }

            var isExist = _unitOfWork.BookRepository.IsExistingBook(request.CreateBookDto.Title);

            if (isExist)
            {
                _logger.LogWarning("Book with Title: {Title} already exists", request.CreateBookDto.Title);
                return BaseResponse<bool>.ConflictResponse($"Book with Title: {request.CreateBookDto.Title} Is Already Exist!!");
            }

            var author = _unitOfWork.AuthorRepository.GetAuthorByNameAsync(request.CreateBookDto.AuthorName);
            var genre = _unitOfWork.GenreRepository.GetGenreByNameAsync(request.CreateBookDto.GenreName);
            var publisher = _unitOfWork.PublisherRepository.GetPublisherByNameAsync(request.CreateBookDto.PublisherName);
            
            var validationResult = await _validator.ValidateAsync(request.CreateBookDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var error = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage));
                _logger.LogError("Validation Error: {Error}", error);
                return BaseResponse<bool>.ValidationErrorResponse(error);
            }

            if (author is null)
            {
                var authorReq = request.CreateBookDto; 
                var newAuthor = new Author
                {
                    Name = authorReq.AuthorName,
                    Biography = authorReq.AuthorBiography,
                    Nationality = authorReq.AuthorNationality,
                    BirthDate = authorReq.AuthorBirthDate,
                    NumberOfBooks = authorReq.AuthorNumberOfBooks
                };
                await _unitOfWork.AuthorRepository.AddAuthorAsync(newAuthor);
                await _unitOfWork.SaveChangesAsync();
            }
            
            if (publisher is null)
            {
                var pubReq = request.CreateBookDto;
                var newPublisher = new Publisher
                {
                    Name = pubReq.PublisherName,
                    Email = pubReq.PublisherEmail,
                    PhoneNumber = pubReq.PublisherPhoneNumber,
                    FoundedDate = pubReq.PublisherFoundedDate
                };
                await _unitOfWork.PublisherRepository.AddPublisherAsync(newPublisher);
                await _unitOfWork.SaveChangesAsync();
            }

            if (genre is null)
            {
                var genreReq = request.CreateBookDto;
                var newGenre = new Genre
                {
                    Name = genreReq.AuthorName,
                    Description = genreReq.GenreDescription,
                    AverageRating = genreReq.GenreAverageRating
                };
                await _unitOfWork.GenreRepository.AddGenreAsync(newGenre);
                await _unitOfWork.SaveChangesAsync();
            }

            var bookReq = request.CreateBookDto;
            
            var newBook = new Book
            {
                Title = bookReq.Title,
                Summary = bookReq.Summary,
                PublishedDate = bookReq.PublishedDate,
                NumberOfPages = bookReq.NumberOfPages,
                Edition = bookReq.Edition,
                Isbn = bookReq.Isbn,
                CoverColor = bookReq.CoverColor,
                BookCoverImage = bookReq.BookCoverImage,
                AuthorId = author!.Id,
                GenreId = genre!.Id,
                PublisherId = publisher!.Id
            };
            await _unitOfWork.BookRepository.AddBookAsync(newBook);
            await _unitOfWork.SaveChangesAsync();
            
            _logger.LogInformation("Book created with Title: {Title}", request.CreateBookDto.Title);

            return BaseResponse<bool>.CreatedResponse(true, $"Book with Title `{request.CreateBookDto.Title}` created successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while creating a Book");

            return BaseResponse<bool>.ErrorResponse("An unexpected error occurred.");
        }
    }
}