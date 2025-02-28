namespace Ambev.DeveloperEvaluation.Application.Cart.ListCarts;

public class ListCartsResult
{
    public List<BaseCartResult> Carts { get; set; } = [];
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public ListCartsResult(List<BaseCartResult> carts, int currentPage, int totalPages, int pageSize, int totalCount)
    {
        Carts = carts;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
