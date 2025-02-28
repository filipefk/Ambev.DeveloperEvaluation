using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using Ambev.DeveloperEvaluation.Domain.Validation.Date;
using Ambev.DeveloperEvaluation.Domain.Validation.Product;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.CreateCart;

public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{
    public CreateCartRequestValidator()
    {
        RuleFor(cart => cart.UserId).SetValidator(new UserIdValidator());
        RuleFor(cart => cart.Date).SetValidator(new StringOnlyDatePtBrValidator());
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
