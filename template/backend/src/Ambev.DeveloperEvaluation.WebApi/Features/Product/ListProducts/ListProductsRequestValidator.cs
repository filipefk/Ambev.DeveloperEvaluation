using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.ListProducts;

public class ListProductsRequestValidator : AbstractValidator<ListProductsRequest>
{
    public ListProductsRequestValidator()
    {

    }
}
