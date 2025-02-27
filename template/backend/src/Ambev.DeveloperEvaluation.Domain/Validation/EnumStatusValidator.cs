using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class EnumStatusValidator : AbstractValidator<UserStatus>
{
    public EnumStatusValidator()
    {
        RuleFor(status => status)
            .IsInEnum().WithMessage("Invalid 'Status'")
            .NotEqual(UserStatus.Unknown).WithMessage("'Status' cannot be 'Unknown'");
    }
}


