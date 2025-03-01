using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale;

public class GetSaleRequestValidator : AbstractValidator<GetSaleRequest>
{
    public GetSaleRequestValidator()
    {
        RuleFor(sale => sale.Id).SetValidator(new SaleIdValidator());
    }
}
