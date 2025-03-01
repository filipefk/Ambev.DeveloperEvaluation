namespace Ambev.DeveloperEvaluation.Application.Sale;

public class BaseSaleDiscountApp
{
    public required Guid ProductSoldId { get; set; }

    public decimal DiscountPercentage { get; set; }

    public decimal DiscountValue { get; set; }

    public string Reason { get; set; } = string.Empty;
}
