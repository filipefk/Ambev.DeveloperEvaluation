using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUsersCommandValidator : AbstractValidator<ListUsersCommand>
{
    public ListUsersCommandValidator()
    {
        RuleFor(command => command.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(command => command.Size).GreaterThan(0).WithMessage("Size must be greater than 0");
        //When(command => !string.IsNullOrEmpty(command.Order), () =>
        //{
        //    RuleFor(command => command.Order).Matches("^(asc|desc)$").WithMessage("Order must be 'asc' or 'desc'");
        //});
    }
}

