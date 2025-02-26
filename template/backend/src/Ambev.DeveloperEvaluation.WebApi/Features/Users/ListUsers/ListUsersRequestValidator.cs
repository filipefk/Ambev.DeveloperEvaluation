using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public class ListUsersRequestValidator : AbstractValidator<ListUsersRequest>
{
    public ListUsersRequestValidator()
    {
        RuleFor(request => request.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
        RuleFor(request => request.Size).GreaterThan(0).WithMessage("Size must be greater than 0");
        When(request => !string.IsNullOrEmpty(request.Order), () =>
        {
            RuleFor(request => request.Order).Matches("^(asc|desc)$").WithMessage("Order must be 'asc' or 'desc'");
        });
    }
}

