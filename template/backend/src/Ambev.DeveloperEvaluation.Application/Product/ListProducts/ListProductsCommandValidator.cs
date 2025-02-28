using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.ListProducts;

public class ListProductsCommandValidator : AbstractValidator<ListProductsCommand>
{
    public ListProductsCommandValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(command => command.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
