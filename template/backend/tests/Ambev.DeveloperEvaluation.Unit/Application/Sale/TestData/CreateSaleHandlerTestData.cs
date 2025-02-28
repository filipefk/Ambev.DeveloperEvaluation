using Ambev.DeveloperEvaluation.Application.Sale;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated sales will have valid:
    /// - CartId
    /// - BranchId
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(c => c.CartId, f => f.Random.Guid())
        .RuleFor(c => c.BranchId, f => f.Random.Guid());

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated sale will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }
}
