using Ambev.DeveloperEvaluation.Application.Cart.GetCart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;

public static class GetCartHandlerTestData
{
    public static GetCartCommand GenerateValidCommand()
    {
        return new GetCartCommand(Guid.NewGuid());
    }

    public static GetCartCommand GenerateInvalidCommand()
    {
        return new GetCartCommand(new Guid());
    }
}