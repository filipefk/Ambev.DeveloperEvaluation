using Ambev.DeveloperEvaluation.Domain.Validation.Product;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(product => product.Id).SetValidator(new ProductIdValidator());
    }
}
