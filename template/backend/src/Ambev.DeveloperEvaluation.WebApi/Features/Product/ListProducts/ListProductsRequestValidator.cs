using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.ListProducts;

public class ListProductsRequestValidator : AbstractValidator<ListProductsRequest>
{
    public ListProductsRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(request => request.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
