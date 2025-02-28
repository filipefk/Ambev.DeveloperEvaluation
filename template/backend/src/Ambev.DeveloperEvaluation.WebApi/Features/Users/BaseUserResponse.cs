namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;
public class BaseUserResponse : BaseUserRequest
{
    /// <summary>
    /// The unique identifier of the created user
    /// </summary>
    public Guid Id { get; set; }

}
