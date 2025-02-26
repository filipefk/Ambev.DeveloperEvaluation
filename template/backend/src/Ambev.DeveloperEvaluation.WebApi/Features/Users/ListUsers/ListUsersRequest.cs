namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;
public class ListUsersRequest
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Order { get; set; }
}

