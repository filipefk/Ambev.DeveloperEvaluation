namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart.UpdateCart;

public class UpdateCartRequest : BaseCartRequest
{
    public Guid Id { get; set; }
    public string Date { get; set; } = string.Empty;
}
