namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.ListCarts;

public class ListCartsRequest
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
}
