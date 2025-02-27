using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Validator for DeleteUserCommand
/// </summary>
public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteUserCommand
    /// </summary>
    public DeleteUserCommandValidator()
    {
        RuleFor(user => user.Id).SetValidator(new UserIdValidator());
    }
}
