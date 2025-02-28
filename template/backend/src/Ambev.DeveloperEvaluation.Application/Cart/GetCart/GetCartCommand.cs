using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.GetCart;

public class GetCartCommand : IRequest<GetCartResult>
{
    /// <summary>
    /// The unique identifier of the product to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetCartCommand
    /// </summary>
    /// <param name="id">The ID of the product to retrieve</param>
    public GetCartCommand(Guid id)
    {
        Id = id;
    }
}
