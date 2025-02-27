using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class UserIdValidator : AbstractValidator<Guid>
{
    public UserIdValidator()
    {
        RuleFor(id => id).NotEmpty().WithMessage("User ID is required");
    }
}


