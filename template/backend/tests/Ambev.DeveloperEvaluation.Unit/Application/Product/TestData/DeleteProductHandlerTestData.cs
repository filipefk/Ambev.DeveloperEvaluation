using Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;

public static class DeleteProductHandlerTestData
{
    public static DeleteProductCommand GenerateValidCommand()
    {
        return new DeleteProductCommand(Guid.NewGuid());
    }

    public static DeleteProductCommand GenerateInvalidCommand()
    {
        return new DeleteProductCommand(new Guid());
    }
}