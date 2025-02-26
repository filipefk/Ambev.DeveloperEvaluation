using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters (using UserNameValidator)
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (using PhoneValidator)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public UpdateUserCommandValidator()
    {
        RuleFor(user => user.Id).NotEmpty().WithMessage("User ID is required");
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).SetValidator(new UserNameValidator());
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).SetValidator(new PhoneValidator());
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown).WithMessage("'Status' cannot be 'Unknown'");
        RuleFor(user => user.Role).NotEqual(UserRole.None).WithMessage("'Role' cannot be 'None'");
    }
}
