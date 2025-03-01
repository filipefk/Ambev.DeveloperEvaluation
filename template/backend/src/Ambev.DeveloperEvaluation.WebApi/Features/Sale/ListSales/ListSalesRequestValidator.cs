using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSales;

public class ListSalesRequestValidator : AbstractValidator<ListSalesRequest>
{
    public ListSalesRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(request => request.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
