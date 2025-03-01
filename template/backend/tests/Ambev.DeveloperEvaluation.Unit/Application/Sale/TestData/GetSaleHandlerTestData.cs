using Ambev.DeveloperEvaluation.Application.Sale.GetSale;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;

public static class GetSaleHandlerTestData
{
    public static GetSaleCommand GenerateValidCommand()
    {
        return new GetSaleCommand(Guid.NewGuid());
    }

    public static GetSaleCommand GenerateInvalidCommand()
    {
        return new GetSaleCommand(new Guid());
    }
}