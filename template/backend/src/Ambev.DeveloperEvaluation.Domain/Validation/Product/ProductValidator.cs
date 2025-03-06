using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Product;

public class ProductValidator : AbstractValidator<Entities.Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Id).SetValidator(new ProductIdValidator());
        RuleFor(product => product.Title).SetValidator(new TitleValidator());
        RuleFor(product => product.Description).SetValidator(new DescriptionValidator());
        RuleFor(product => product.Price).SetValidator(new PriceValidator());
        RuleFor(product => product.Category).SetValidator(new CategoryValidator());
        RuleFor(product => product.Rating.Count).SetValidator(new RatingCountValidator());
        RuleFor(product => product.Rating.Rate).SetValidator(new RatingRateValidator());
    }

}
