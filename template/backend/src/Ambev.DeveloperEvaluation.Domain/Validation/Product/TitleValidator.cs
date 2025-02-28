using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class TitleValidator : AbstractValidator<string>
{
    public TitleValidator()
    {
        RuleFor(title => title)
            .NotEmpty().WithMessage("The Title cannot be empty.")
            .MaximumLength(100).WithMessage("The Title cannot exceed 100 characters.");
    }
}
