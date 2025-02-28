using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;

public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartCommandValidator()
    {
        RuleFor(product => product.Id).SetValidator(new CartIdValidator());
        RuleFor(product => product.UserId).SetValidator(new UserIdValidator());

    }
}
