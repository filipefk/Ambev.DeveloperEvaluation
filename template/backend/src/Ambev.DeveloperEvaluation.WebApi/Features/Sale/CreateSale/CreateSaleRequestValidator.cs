using Ambev.DeveloperEvaluation.Domain.Validation.Branch;
using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.CartId).SetValidator(new CartIdValidator());
        RuleFor(sale => sale.BranchId).SetValidator(new BranchIdValidator());
    }
}
