using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class PriceValidator : AbstractValidator<decimal>
{
    public PriceValidator()
    {
        RuleFor(price => price)
            .GreaterThan(0).WithMessage("The Price must be greater than zero.");
    }
}
