using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Command for list users.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for list users.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="ListUsersResult"/>.
/// </remarks>
public class ListUsersCommand : IRequest<ListUsersResult>
{
    /// <summary>
    /// Gets or sets the page number for pagination. Default is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the number of users to retrieve per page. Default is 10.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the order in which to list the users. Can be null.
    /// </summary>
    public string? Order { get; set; }

}

