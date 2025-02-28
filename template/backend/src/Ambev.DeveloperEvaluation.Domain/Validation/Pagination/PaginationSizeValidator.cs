using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Pagination;

public class PaginationSizeValidator : AbstractValidator<int>
{
    public PaginationSizeValidator()
    {
        RuleFor(page => page).GreaterThan(0).WithMessage("Page must be greater than 0");
    }
}
