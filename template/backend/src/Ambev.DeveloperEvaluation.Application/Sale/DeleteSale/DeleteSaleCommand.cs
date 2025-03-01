using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

public class DeleteSaleCommand : IRequest<DeleteSaleResult>
{
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteSaleCommand
    /// </summary>
    /// <param name="id">The ID of the product to delete</param>
    public DeleteSaleCommand(Guid id)
    {
        Id = id;
    }
}
