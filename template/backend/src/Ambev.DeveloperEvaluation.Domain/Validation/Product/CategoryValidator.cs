using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class CategoryValidator : AbstractValidator<string>
{
    public CategoryValidator()
    {
        RuleFor(category => category)
            .NotEmpty().WithMessage("The Category cannot be empty.")
            .MaximumLength(100).WithMessage("The Category cannot exceed 100 characters.");
    }
}
