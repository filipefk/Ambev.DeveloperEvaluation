using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace TestUtil.Entities;

public static class ProductBuilder
{
    public static Product Build()
    {
        return new Faker<Product>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(p => p.Title, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, (f, p) => decimal.Parse(f.Commerce.Price()))
            .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
            .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
            .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
            .RuleFor(p => p.Rating, f => new Rating
            {
                Rate = f.Random.Decimal(1, 5),
                Count = f.Random.Int(1, 1000)
            });
    }
}
