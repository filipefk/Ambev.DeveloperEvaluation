using Ambev.DeveloperEvaluation.Domain.Validation.Branch;
using Ambev.DeveloperEvaluation.Domain.Validation.Cart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.CartId).SetValidator(new CartIdValidator());
        RuleFor(sale => sale.BranchId).SetValidator(new BranchIdValidator());
    }
}
