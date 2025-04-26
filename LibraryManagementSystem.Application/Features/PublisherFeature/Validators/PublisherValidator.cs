using FluentValidation;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;

namespace LibraryManagementSystem.Application.Features.PublisherFeature.Validators;

public abstract class PublisherValidator: AbstractValidator<PublisherDto>
{
    protected PublisherValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Publisher name is required.")
            .MaximumLength(30).WithMessage("Publisher name must not exceed 30 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{7,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.FoundedDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Founded date must be in the past.");

        RuleFor(x => x.NumberOfBooksPublished)
            .GreaterThanOrEqualTo(0).WithMessage("Number of books published cannot be negative.");
    }
}