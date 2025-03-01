namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

public class BaseSaleProductApi
{
    public Guid ProductId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal SoldPrice { get; set; }

    public decimal TotalAmount
    {
        get
        {
            return Quantity * SoldPrice;
        }
    }

    public int Quantity { get; set; }
}
