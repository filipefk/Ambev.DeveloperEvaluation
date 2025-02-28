using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Cart;

public class CartProductQuantityValidator : AbstractValidator<int>
{
    public CartProductQuantityValidator()
    {
        RuleFor(quantity => quantity)
            .GreaterThan(0).WithMessage("The cart product quantity must be greater than zero.");
    }

}
