using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.Username).SetValidator(new UserNameValidator());
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).SetValidator(new PhoneValidator());
        RuleFor(user => user.Status).NotEqual(UserStatus.Unknown).WithMessage("'Status' cannot be 'Unknown'");
        RuleFor(user => user.Role).NotEqual(UserRole.None).WithMessage("'Role' cannot be 'None'");
    }
}
