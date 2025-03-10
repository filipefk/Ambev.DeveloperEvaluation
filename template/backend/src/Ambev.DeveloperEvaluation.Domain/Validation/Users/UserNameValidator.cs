﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Users;

public class UserNameValidator : AbstractValidator<string>
{
    public UserNameValidator()
    {
        RuleFor(userName => userName)
        .NotEmpty().WithMessage("Username must be at least 3 characters long.")
        .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
        .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");
    }
}

