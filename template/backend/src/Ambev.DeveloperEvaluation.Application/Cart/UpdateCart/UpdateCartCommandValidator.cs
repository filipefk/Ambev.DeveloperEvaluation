using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using Ambev.DeveloperEvaluation.Domain.Validation.Product;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;

public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartCommandValidator()
    {
        RuleFor(cart => cart.Id).SetValidator(new CartIdValidator());
        RuleFor(cart => cart.UserId).SetValidator(new UserIdValidator());
        RuleFor(cart => cart.Date).NotEmpty().WithMessage("Date cannot be empty");

        RuleFor(cart => cart.Products).NotNull().WithMessage("Products cannot be null");
        When(cart => cart.Products != null, () =>
        {
            RuleFor(cart => cart.Products.Count).GreaterThan(0).WithMessage("Products cannot be empty");
            RuleForEach(cart => cart.Products).ChildRules(productRule =>
            {
                productRule.RuleFor(product => product.ProductId).SetValidator(new ProductIdValidator());
                productRule.RuleFor(product => product.Quantity).SetValidator(new CartProductQuantityValidator());
            });
        });
    }
}
