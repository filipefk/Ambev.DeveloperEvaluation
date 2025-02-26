namespace Ambev.DeveloperEvaluation.Domain.Common;
public class PaginatedResult<T>
{
    public List<T> Results { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public PaginatedResult(List<T> results, int currentPage, int pageSize, int totalCount)
    {
        Results = results;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
