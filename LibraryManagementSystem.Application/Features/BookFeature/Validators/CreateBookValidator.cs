using FluentValidation;
using LibraryManagementSystem.Application.Features.BookFeature.DTOs;

namespace LibraryManagementSystem.Application.Features.BookFeature.Validators;

public class CreateBookValidator: AbstractValidator<CreateBookDto>
{
    public CreateBookValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(30).WithMessage("Title can't exceed 30 characters.")
            .MinimumLength(3).WithMessage("Title can't less than 3 characters.");
        
        RuleFor(book => book.Summary)
            .NotEmpty().WithMessage("Summary is required.")
            .MaximumLength(800).WithMessage("Summary can't exceed 800 characters.")
            .MinimumLength(100).WithMessage("Summary can't less than 100 characters.");
        
        RuleFor(book => book.PublishedDate)
            .NotEmpty().WithMessage("Published date is required.");

        RuleFor(book => book.NumberOfPages)
            .GreaterThan(0).WithMessage("Number of pages must be greater than 0.");

        RuleFor(book => book.Edition)
            .GreaterThan(0).WithMessage("Edition must be greater than 0.");

        RuleFor(book => book.Isbn)
            .NotEmpty().WithMessage("ISBN is required.")
            .Length(10, 13).WithMessage("ISBN must be between 10 and 13 characters.");

        RuleFor(book => book.CoverColor)
            .NotEmpty().WithMessage("Cover color is required.");
        
        RuleFor(book => book.AuthorName)
            .NotEmpty().WithMessage("Author name is required.");

        RuleFor(book => book.GenreName)
            .NotEmpty().WithMessage("Genre name is required.");
        
        RuleFor(book => book.PublisherName)
            .NotEmpty().WithMessage("Publisher name is required.");
    }
}