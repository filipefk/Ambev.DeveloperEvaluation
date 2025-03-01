namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

public class ListSalesResult
{
    public List<BaseSaleResult> Sales { get; set; } = [];
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public ListSalesResult(List<BaseSaleResult> sales, int currentPage, int totalPages, int pageSize, int totalCount)
    {
        Sales = sales;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
