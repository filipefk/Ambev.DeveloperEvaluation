using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.ListSales;

/// <summary>
/// Command for list sales.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for list sales.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="ListSalesResult"/>.
/// </remarks>
public class ListSalesCommand : IRequest<ListSalesResult>
{
    /// <summary>
    /// Gets or sets the page number for pagination. Default is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of sales to retrieve per page. Default is 10.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the order in which to list the sales. Can be null.
    /// </summary>
    public string? Order { get; set; }

}
