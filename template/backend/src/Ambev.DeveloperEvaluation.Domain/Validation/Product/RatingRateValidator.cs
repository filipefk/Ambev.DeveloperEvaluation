using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class RatingRateValidator : AbstractValidator<decimal>
{
    public RatingRateValidator()
    {
        RuleFor(rate => rate)
            .GreaterThan(-1).WithMessage("The Rating Rate cannot be less than zero.");
    }
}
