using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class RatingCountValidator : AbstractValidator<int>
{
    public RatingCountValidator()
    {
        RuleFor(count => count)
            .GreaterThan(-1).WithMessage("The Rating Count cannot be less than zero.");
    }

}
