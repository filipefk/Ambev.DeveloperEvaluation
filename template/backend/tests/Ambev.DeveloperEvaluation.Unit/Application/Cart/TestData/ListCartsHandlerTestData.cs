using Ambev.DeveloperEvaluation.Application.Cart.ListCarts;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Unit.Application.Cart.TestData;

public static class ListCartsHandlerTestData
{
    public static ListCartsCommand GenerateValidCommand()
    {
        return new ListCartsCommand
        {
            Page = 1,
            Size = 10,
            Order = "CartName asc"
        };
    }

    public static ListCartsCommand GenerateInvalidCommand()
    {
        return new ListCartsCommand
        {
            Page = 0,
            Size = 0,
            Order = ""
        };
    }

    public static PaginatedResult<DeveloperEvaluation.Domain.Entities.Cart> GeneratePaginatedResultCarts(int totalCount, int currentPage, int pageSize)
    {
        var carts = new List<DeveloperEvaluation.Domain.Entities.Cart>();

        var listCount = totalCount > pageSize ? pageSize : totalCount;

        for (var i = 0; i < listCount; i++)
        {
            carts.Add(CartTestData.GenerateValidCart());
        }

        return new PaginatedResult<DeveloperEvaluation.Domain.Entities.Cart>(carts, currentPage, pageSize, totalCount);

    }
}