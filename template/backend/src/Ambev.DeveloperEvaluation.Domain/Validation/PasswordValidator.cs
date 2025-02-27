using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty()
            .MinimumLength(8).WithMessage(password => $"The password must be at least 8 characters long. You have entered {password.Length} characters.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&\+\=]+").WithMessage("Password must contain at least one special character.");
    }
}