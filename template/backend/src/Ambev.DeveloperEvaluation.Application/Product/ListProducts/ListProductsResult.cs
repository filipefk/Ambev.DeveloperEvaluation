namespace Ambev.DeveloperEvaluation.Application.Product.ListProducts;

public class ListProductsResult
{
    public List<BaseProductResult> Products { get; set; } = [];
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public ListProductsResult(List<BaseProductResult> products, int currentPage, int totalPages, int pageSize, int totalCount)
    {
        Products = products;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
