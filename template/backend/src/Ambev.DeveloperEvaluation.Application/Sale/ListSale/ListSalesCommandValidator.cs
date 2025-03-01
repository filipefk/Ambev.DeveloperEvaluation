using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

public class ListSalesCommandValidator : AbstractValidator<ListSalesCommand>
{
    public ListSalesCommandValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(command => command.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
