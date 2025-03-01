using Ambev.DeveloperEvaluation.Domain.Validation.Branch;
using Ambev.DeveloperEvaluation.Domain.Validation.Date;
using Ambev.DeveloperEvaluation.Domain.Validation.Product;
using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.UserId).SetValidator(new UserIdValidator());
        RuleFor(sale => sale.BranchId).SetValidator(new BranchIdValidator());

        RuleFor(sale => sale.Date).SetValidator(new StringOnlyDatePtBrValidator());
        RuleFor(sale => sale.Products).NotNull().WithMessage("Products cannot be null");
        When(sale => sale.Products != null, () =>
        {
            RuleFor(sale => sale.Products.Count).GreaterThan(0).WithMessage("Products cannot be empty");
            RuleForEach(sale => sale.Products).ChildRules(productRule =>
            {
                productRule.RuleFor(product => product.ProductId).SetValidator(new ProductIdValidator());
                productRule.RuleFor(product => product.Quantity).SetValidator(new SaleProductQuantityValidator());
            });
        });
    }
}
