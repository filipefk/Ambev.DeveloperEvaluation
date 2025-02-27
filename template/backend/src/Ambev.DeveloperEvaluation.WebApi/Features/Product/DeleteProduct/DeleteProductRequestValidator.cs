using Ambev.DeveloperEvaluation.Domain.Validation.Product;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.DeleteProduct;

public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(product => product.Id).SetValidator(new ProductIdValidator());
    }
}
