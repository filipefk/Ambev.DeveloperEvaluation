namespace Ambev.DeveloperEvaluation.WebApi.Features.Product.ListProducts;

public class ListProductsRequest
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
}
