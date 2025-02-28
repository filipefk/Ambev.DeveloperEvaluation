using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCarts;

public class ListCartsRequestValidator : AbstractValidator<ListCartsRequest>
{
    public ListCartsRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(request => request.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
