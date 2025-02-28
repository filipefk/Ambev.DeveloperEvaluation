using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.GetCart;

public class GetCartRequestValidator : AbstractValidator<GetCartRequest>
{
    public GetCartRequestValidator()
    {
        RuleFor(cart => cart.Id).SetValidator(new CartIdValidator());
    }
}
