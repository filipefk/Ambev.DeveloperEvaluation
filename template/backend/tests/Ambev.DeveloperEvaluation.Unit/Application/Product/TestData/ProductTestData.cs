using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have valid:
    /// - Title
    /// - Price
    /// - Description
    /// - Category
    /// - Image
    /// - Rating (Rate and Count)
    /// </summary>
    private static readonly Faker<DeveloperEvaluation.Domain.Entities.Product> createProductFaker = new Faker<DeveloperEvaluation.Domain.Entities.Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, (f, p) => decimal.Parse(f.Commerce.Price()))
        .RuleFor(p => p.Description, f => f.Lorem.Paragraph())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.Rating, (f, p) => new DeveloperEvaluation.Domain.Entities.Rating
        {
            Rate = f.Random.Decimal(1, 5),
            Count = f.Random.Int(1, 1000),
            Product = p
        });


    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated Product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static DeveloperEvaluation.Domain.Entities.Product GenerateValidProduct()
    {
        return createProductFaker.Generate();
    }
}
