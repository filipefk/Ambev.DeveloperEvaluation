using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.ListProducts;

public class ListProductsRequestValidator : AbstractValidator<ListProductsRequest>
{
    public ListProductsRequestValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(command => command.Size).GreaterThan(0).WithMessage("Size must be greater than 0");
    }
}
