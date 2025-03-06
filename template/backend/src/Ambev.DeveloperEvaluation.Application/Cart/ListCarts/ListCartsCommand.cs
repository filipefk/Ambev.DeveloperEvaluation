using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Cart.ListCarts;

/// <summary>
/// Command for list carts.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for list carts.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="ListCartsResult"/>.
/// </remarks>
public class ListCartsCommand : IRequest<ListCartsResult>
{
    /// <summary>
    /// Gets or sets the page number for pagination. Default is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of carts to retrieve per page. Default is 10.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the order in which to list the carts. Can be null.
    /// </summary>
    public string? Order { get; set; }

}
