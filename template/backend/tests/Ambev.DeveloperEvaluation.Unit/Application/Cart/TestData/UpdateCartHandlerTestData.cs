using Ambev.DeveloperEvaluation.Application.Cart;
using Ambev.DeveloperEvaluation.Application.Cart.UpdateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateCartHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid UpdateCartCommand entities.
    /// The generated commands will have valid:
    /// - Id
    /// - UserId
    /// - Date
    /// - Products
    /// </summary>
    private static readonly Faker<UpdateCartCommand> updateCartHandlerFaker = new Faker<UpdateCartCommand>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.Date, f => f.Date.Past())
        .RuleFor(c => c.Products, f => new List<BaseCartProductApp>
        {
            new Faker<BaseCartProductApp>()
                .RuleFor(p => p.ProductId, f => f.Random.Guid())
                .RuleFor(p => p.Quantity, f => f.Random.Int(1, 10))
                .Generate()
        });

    /// <summary>
    /// Generates a valid UpdateCartCommand with randomized data.
    /// The generated UpdateCartCommand will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid UpdateCartCommand with randomly generated data.</returns>
    public static UpdateCartCommand GenerateValidCommand()
    {
        return updateCartHandlerFaker.Generate();
    }
}
