using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Cart
{
    public class CartIdValidator : AbstractValidator<Guid>
    {
        public CartIdValidator()
        {
            RuleFor(id => id).NotEmpty().WithMessage("Cart ID is required");
        }

    }

}
