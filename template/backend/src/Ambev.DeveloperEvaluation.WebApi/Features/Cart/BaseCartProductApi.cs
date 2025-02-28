namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart;

public class BaseCartProductApi
{
    public Guid ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }
}
