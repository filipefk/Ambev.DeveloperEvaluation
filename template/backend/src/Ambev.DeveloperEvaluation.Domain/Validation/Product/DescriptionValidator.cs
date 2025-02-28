using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class DescriptionValidator : AbstractValidator<string>
{
    public DescriptionValidator()
    {
        RuleFor(description => description)
            .NotEmpty().WithMessage("The Description cannot be empty.")
            .MaximumLength(500).WithMessage("The Description cannot exceed 500 characters.");
    }
}
