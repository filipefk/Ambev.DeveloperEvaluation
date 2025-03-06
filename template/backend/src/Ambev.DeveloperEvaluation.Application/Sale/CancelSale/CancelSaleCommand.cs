using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class CancelSaleCommand : IRequest<CancelSaleResult>
{
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of CancelSaleCommand
    /// </summary>
    /// <param name="id">The ID of the product to delete</param>
    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }
}
