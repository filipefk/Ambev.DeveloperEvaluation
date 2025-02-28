using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Cart.ListCarts;

public class ListCartsCommandValidator : AbstractValidator<ListCartsCommand>
{
    public ListCartsCommandValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(command => command.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
