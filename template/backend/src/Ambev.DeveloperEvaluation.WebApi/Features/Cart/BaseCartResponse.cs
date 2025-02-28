namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart;

public class BaseCartResponse : BaseCartRequest
{
    public Guid Id { get; set; }

    public string Date { get; set; } = string.Empty;

}
