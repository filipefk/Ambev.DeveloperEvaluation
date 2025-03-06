using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListBranches;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.ListBranches;

public class ListBranchesRequestValidator : AbstractValidator<ListBranchesRequest>
{
    public ListBranchesRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(request => request.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}
