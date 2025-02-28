using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
{
    public DeleteSaleCommandValidator()
    {
        RuleFor(sale => sale.Id).SetValidator(new SaleIdValidator());
    }
}
