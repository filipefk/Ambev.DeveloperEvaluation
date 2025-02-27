using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation.Users;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be valid format (using EmailValidator)
    /// - Username: Required, length between 3 and 50 characters (using UserNameValidator)
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (using PhoneValidator)
    /// - Status: Cannot be Unknown
    /// - Role: Cannot be None
    /// </remarks>
    public UpdateUserRequestValidator()
    {
        RuleFor(user => user.Email).SetValidator(new EmailValidator());
        RuleFor(user => user.UserName).SetValidator(new UserNameValidator());
        RuleFor(user => user.Password).SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone).SetValidator(new PhoneValidator());
        RuleFor(user => user.Status)
            .Must(EnumValidatorUtil.BeAValidEnumString<UserStatus>).WithMessage($"'Status' must be a valid value: {EnumValidatorUtil.GetEnumNames<UserStatus>("Unknown")}")
            .NotEqual(UserStatus.Unknown.ToString()).WithMessage("'Status' cannot be 'Unknown'");
        RuleFor(user => user.Role)
            .Must(EnumValidatorUtil.BeAValidEnumString<UserRole>).WithMessage($"'Role' must be a valid value: {EnumValidatorUtil.GetEnumNames<UserRole>("None")}")
            .NotEqual(UserRole.None.ToString()).WithMessage("'Role' cannot be 'None'");
    }

}
