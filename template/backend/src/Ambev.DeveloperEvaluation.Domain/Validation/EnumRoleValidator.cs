using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class EnumRoleValidator : AbstractValidator<UserRole>
{
    public EnumRoleValidator()
    {
        RuleFor(role => role)
            .IsInEnum().WithMessage("Invalid 'Role'")
            .NotEqual(UserRole.None).WithMessage("'Role' cannot be 'None'");
    }

}


