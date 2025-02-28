using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Branch;

public class BranchIdValidator : AbstractValidator<Guid>
{
    public BranchIdValidator()
    {
        RuleFor(id => id).NotEmpty().WithMessage("Branch ID is required");
    }
}


