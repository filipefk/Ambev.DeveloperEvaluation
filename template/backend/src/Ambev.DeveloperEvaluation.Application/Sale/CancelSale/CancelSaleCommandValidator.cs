using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
{
    public CancelSaleCommandValidator()
    {
        RuleFor(sale => sale.Id).SetValidator(new SaleIdValidator());
    }
}
