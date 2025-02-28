using FluentValidation;
using System.Globalization;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Date;

public class StringOnlyDatePtBrValidator : AbstractValidator<string>
{
    public StringOnlyDatePtBrValidator()
    {
        RuleFor(stringDate => stringDate)
            .NotEmpty().WithMessage("The Date cannot be empty.")
            .Must(BeAValidDate).WithMessage("The Date must be in the format dd/MM/yyyy.");
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
