using Ambev.DeveloperEvaluation.Domain.Validation.Pagination;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersCommandValidator : AbstractValidator<ListUsersCommand>
{
    public ListUsersCommandValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).SetValidator(new PaginationPageValidator());
        RuleFor(command => command.Size).GreaterThan(0).SetValidator(new PaginationSizeValidator());
    }
}

