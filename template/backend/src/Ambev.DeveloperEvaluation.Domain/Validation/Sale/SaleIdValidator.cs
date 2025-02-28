using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sale
{
    public class SaleIdValidator : AbstractValidator<Guid>
    {
        public SaleIdValidator()
        {
            RuleFor(id => id).NotEmpty().WithMessage("Sale ID is required");
        }

    }

}
