using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersCommandValidator : AbstractValidator<ListUsersCommand>
{
    public ListUsersCommandValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(command => command.Size).GreaterThan(0).WithMessage("Size must be greater than 0");
    }
}

