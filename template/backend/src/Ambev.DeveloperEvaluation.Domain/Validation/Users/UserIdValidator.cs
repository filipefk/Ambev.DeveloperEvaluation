using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Users;

public class UserIdValidator : AbstractValidator<Guid>
{
    public UserIdValidator()
    {
        RuleFor(id => id).NotEmpty().WithMessage("User ID is required");
    }
}


