using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.DeleteCart;

public class DeleteCartCommand : IRequest<DeleteCartResult>
{
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteCartCommand
    /// </summary>
    /// <param name="id">The ID of the product to delete</param>
    public DeleteCartCommand(Guid id)
    {
        Id = id;
    }
}
