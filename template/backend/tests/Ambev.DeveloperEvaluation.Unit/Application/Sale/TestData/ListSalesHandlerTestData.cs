using Ambev.DeveloperEvaluation.Application.Sale.ListSales;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale.TestData;

public static class ListSalesHandlerTestData
{
    public static ListSalesCommand GenerateValidCommand()
    {
        return new ListSalesCommand
        {
            Page = 1,
            Size = 10,
            Order = ""
        };
    }

    public static ListSalesCommand GenerateInvalidCommand()
    {
        return new ListSalesCommand
        {
            Page = 0,
            Size = 0,
            Order = ""
        };
    }

    public static PaginatedResult<DeveloperEvaluation.Domain.Entities.Sale> GeneratePaginatedResultSales(int totalCount, int currentPage, int pageSize)
    {
        var sales = new List<DeveloperEvaluation.Domain.Entities.Sale>();

        var listCount = totalCount > pageSize ? pageSize : totalCount;

        for (var i = 0; i < listCount; i++)
        {
            sales.Add(SaleTestData.GenerateValidSale());
        }

        return new PaginatedResult<DeveloperEvaluation.Domain.Entities.Sale>(sales, currentPage, pageSize, totalCount);

    }
}