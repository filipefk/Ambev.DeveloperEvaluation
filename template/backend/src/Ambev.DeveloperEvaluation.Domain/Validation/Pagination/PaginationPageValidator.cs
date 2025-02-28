using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Pagination;

public class PaginationPageValidator : AbstractValidator<int>
{
    public PaginationPageValidator()
    {
        RuleFor(page => page).GreaterThan(0).WithMessage("Size must be greater than 0");
    }
}
