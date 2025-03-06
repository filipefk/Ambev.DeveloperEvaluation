namespace Ambev.DeveloperEvaluation.Application.Branch.ListBranches;

public class ListBranchesResult
{
    public List<BaseBranchResult> Branches { get; set; } = [];
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }

    public ListBranchesResult(List<BaseBranchResult> branches, int currentPage, int totalPages, int pageSize, int totalCount)
    {
        Branches = branches;
        CurrentPage = currentPage;
        TotalPages = totalPages;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
