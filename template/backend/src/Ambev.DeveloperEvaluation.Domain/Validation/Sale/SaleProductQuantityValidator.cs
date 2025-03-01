using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sale;

public class SaleProductQuantityValidator : AbstractValidator<int>
{
    public SaleProductQuantityValidator()
    {
        RuleFor(quantity => quantity)
            .GreaterThan(0).WithMessage("The sale product quantity must be greater than zero.");
    }

}
