using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class ProductIdValidator : AbstractValidator<Guid>
{
    public ProductIdValidator()
    {
        RuleFor(id => id).NotEmpty().WithMessage("Product ID is required");
    }

}
