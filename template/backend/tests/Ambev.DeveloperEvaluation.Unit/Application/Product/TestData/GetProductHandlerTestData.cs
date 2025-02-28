using Ambev.DeveloperEvaluation.Application.Product.GetProduct;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

public static class GetProductHandlerTestData
{
    public static GetProductCommand GenerateValidCommand()
    {
        return new GetProductCommand(Guid.NewGuid());
    }

    public static GetProductCommand GenerateInvalidCommand()
    {
        return new GetProductCommand(new Guid());
    }
}