using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public required Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid BranchId { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public ICollection<BaseSaleProductApp> Products { get; set; } = [];

    public bool Canceled { get; set; } = false;
}
