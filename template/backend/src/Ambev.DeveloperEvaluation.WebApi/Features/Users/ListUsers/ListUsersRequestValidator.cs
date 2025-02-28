using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
{
    public ListUsersRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(request => request.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}

