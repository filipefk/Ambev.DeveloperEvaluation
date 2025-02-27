using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator()
    {
        RuleFor(phone => phone)
            .NotEmpty().WithMessage("The phone cannot be empty.")
            .Matches(@"^\+?[1-9]\d{8,14}$")
            .WithMessage("The phone number can only contain digits, it can start with '+', the first digit cannot be zero, and it must be between 9 and 15 digits long.");
    }
}
