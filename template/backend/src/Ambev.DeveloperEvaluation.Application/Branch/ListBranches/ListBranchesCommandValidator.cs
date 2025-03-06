using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

public class ListBranchesCommandValidator : AbstractValidator<ListBranchesCommand>
{
    public ListBranchesCommandValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(command => command.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
