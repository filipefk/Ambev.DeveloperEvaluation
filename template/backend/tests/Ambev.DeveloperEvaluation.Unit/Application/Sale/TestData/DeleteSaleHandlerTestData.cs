using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;

public static class DeleteSaleHandlerTestData
{
    public static DeleteSaleCommand GenerateValidCommand()
    {
        return new DeleteSaleCommand(Guid.NewGuid());
    }

    public static DeleteSaleCommand GenerateInvalidCommand()
    {
        return new DeleteSaleCommand(new Guid());
    }
}