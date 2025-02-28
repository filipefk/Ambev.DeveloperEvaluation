using Ambev.DeveloperEvaluation.Application.Cart.DeleteCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;

public static class DeleteCartHandlerTestData
{
    public static DeleteCartCommand GenerateValidCommand()
    {
        return new DeleteCartCommand(Guid.NewGuid());
    }

    public static DeleteCartCommand GenerateInvalidCommand()
    {
        return new DeleteCartCommand(new Guid());
    }
}