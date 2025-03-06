using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CancelSale;

public class CancelSaleRequestValidator : AbstractValidator<CancelSaleRequest>
{
    public CancelSaleRequestValidator()
    {
        RuleFor(sale => sale.Id).SetValidator(new SaleIdValidator());
    }
}
