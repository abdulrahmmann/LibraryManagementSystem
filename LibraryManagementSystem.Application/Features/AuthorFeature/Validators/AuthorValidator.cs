using FluentValidation;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Author name cannot be empty.")
                .NotNull().WithMessage("Author name is required.")
                .Length(3, 30).WithMessage("Author name must be between 3 and 30 characters long.");


            RuleFor(a => a.Biography)
                .NotEmpty().WithMessage("Author name cannot be empty.")
                .NotNull().WithMessage("Author name is required.")
                .Length(15, 300).WithMessage("Biography must be between 15 and 300 characters.");

            RuleFor(a => a.Nationality)
            .NotEmpty().WithMessage("Nationality is required.")
            .MaximumLength(50).WithMessage("Nationality cannot exceed 50 characters.");

            RuleFor(a => a.BirthDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Birth date cannot be in the future.");
        }
    }
}
