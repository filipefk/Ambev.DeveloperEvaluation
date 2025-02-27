using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;

public class DeleteProductCommand : IRequest<DeleteProductResult>
{
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteProductCommand
    /// </summary>
    /// <param name="id">The ID of the product to delete</param>
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
