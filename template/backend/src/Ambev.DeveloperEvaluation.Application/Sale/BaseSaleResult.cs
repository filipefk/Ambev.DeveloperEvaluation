namespace Ambev.DeveloperEvaluation.Application.Sale;

public class BaseSaleResult
{
    public required Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid BranchId { get; set; }

    public long SaleNumber { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ICollection<BaseSaleProductApp> Products { get; set; } = [];

    public ICollection<BaseSaleDiscountApp> Discounts { get; set; } = [];

    public decimal SaleTotal { get; set; }

    public bool Canceled { get; set; } = false;

}
