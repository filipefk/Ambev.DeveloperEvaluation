namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.ListSales;

public class ListSalesRequest
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
}
