using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetUserCommandValidator : AbstractValidator<GetUserCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    public GetUserCommandValidator()
    {
        RuleFor(user => user.Id).SetValidator(new UserIdValidator());
    }
}
