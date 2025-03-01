using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

public class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleCommandValidator()
    {
        RuleFor(sale => sale.Id).SetValidator(new SaleIdValidator());
    }
}
