using FluentValidation;
using LibraryManagementSystem.Application.Features.GenreFeature.DTOs;

namespace LibraryManagementSystem.Application.Features.GenreFeature.Validators;

public class GenreValidator: AbstractValidator<GenreDTO>
{
    public GenreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .MaximumLength(20).WithMessage("Genre name must not exceed 20 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("Description must not exceed 150 characters.");

        RuleFor(x => x.AverageRating)
            .InclusiveBetween(0, 5).WithMessage("Average rating must be between 0 and 5.");
    }
}