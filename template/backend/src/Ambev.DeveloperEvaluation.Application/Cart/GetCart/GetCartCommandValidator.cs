using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Cart.GetCart;

public class GetCartCommandValidator : AbstractValidator<GetCartCommand>
{
    public GetCartCommandValidator()
    {
        RuleFor(product => product.Id).SetValidator(new CartIdValidator());
    }
}
