using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.DeleteCart;

public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
{
    public DeleteCartRequestValidator()
    {
        RuleFor(cart => cart.Id).SetValidator(new CartIdValidator());
    }
}
