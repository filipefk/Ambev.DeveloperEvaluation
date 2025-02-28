using Ambev.DeveloperEvaluation.Application.Product.ListProducts;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

public static class ListProductsHandlerTestData
{
    public static ListProductsCommand GenerateValidCommand()
    {
        return new ListProductsCommand
        {
            Page = 1,
            Size = 10,
            Order = "ProductName asc"
        };
    }

    public static ListProductsCommand GenerateInvalidCommand()
    {
        return new ListProductsCommand
        {
            Page = 0,
            Size = 0,
            Order = ""
        };
    }

    public static PaginatedResult<DeveloperEvaluation.Domain.Entities.Product> GeneratePaginatedResultProducts(int totalCount, int currentPage, int pageSize)
    {
        var products = new List<DeveloperEvaluation.Domain.Entities.Product>();

        var listCount = totalCount > pageSize ? pageSize : totalCount;

        for (var i = 0; i < listCount; i++)
        {
            products.Add(ProductTestData.GenerateValidProduct());
        }

        return new PaginatedResult<DeveloperEvaluation.Domain.Entities.Product>(products, currentPage, pageSize, totalCount);

    }
}