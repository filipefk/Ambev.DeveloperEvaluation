namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale;

public class BaseSaleResponse
{
    public Guid Id { get; set; }

    public string Date { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Guid BranchId { get; set; }

    public long SaleNumber { get; set; }

    public ICollection<BaseSaleProductApi> Products { get; set; } = [];

    public ICollection<BaseSaleDiscountApi> Discounts { get; set; } = [];

    public decimal SaleTotal { get; set; }

    public bool Canceled { get; set; } = false;

}
