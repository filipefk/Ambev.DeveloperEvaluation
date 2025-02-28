using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public Guid CartId { get; set; }

    public Guid BranchId { get; set; }
}
