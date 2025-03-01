namespace Ambev.DeveloperEvaluation.Application.Sale;

public class BaseSaleProductApp
{
    public Guid ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal SoldPrice { get; set; }

    public string Category { get; set; } = string.Empty;
}
