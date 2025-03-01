using Ambev.DeveloperEvaluation.Application.Sale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale;

public class UpdateSaleRequest
{
    public required Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid BranchId { get; set; }

    public string Date { get; set; } = string.Empty;

    public ICollection<BaseSaleProductApp> Products { get; set; } = [];

    public bool Canceled { get; set; } = false;
}
