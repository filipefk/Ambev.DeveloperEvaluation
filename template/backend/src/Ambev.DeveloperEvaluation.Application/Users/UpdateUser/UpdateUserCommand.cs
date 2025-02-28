using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Command for update a user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for update a user, 
/// including username, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateUserResult"/>.
/// </remarks>
public class UpdateUserCommand : BaseUserCommand, IRequest<UpdateUserResult>
{
    /// <summary>
    /// The unique identifier of the user to update
    /// </summary>
    public Guid Id { get; set; }
}

