using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CartTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart entities.
    /// The generated carts will have valid:
    /// - UserId
    /// - Date
    /// - Products
    /// - UpdatedAt
    /// - User
    /// </summary>
    private static readonly Faker<DeveloperEvaluation.Domain.Entities.Cart> createCartFaker = new Faker<DeveloperEvaluation.Domain.Entities.Cart>()
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.Date, f => f.Date.Past())
        .RuleFor(c => c.Products, f => new List<CartProduct>
        {
            new Faker<CartProduct>()
                .RuleFor(p => p.ProductId, f => f.Random.Guid())
                .RuleFor(p => p.CartId, f => f.Random.Guid())
                .RuleFor(p => p.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past())
                .RuleFor(p => p.UpdatedAt, f => f.Date.Recent())
                .RuleFor(p => p.Product, _ => null!)
                .Generate()
        })
        .RuleFor(c => c.UpdatedAt, f => f.Date.Recent())
        .RuleFor(c => c.User, _ => null!);

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated Cart will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static DeveloperEvaluation.Domain.Entities.Cart GenerateValidCart()
    {
        return createCartFaker.Generate();
    }
}
