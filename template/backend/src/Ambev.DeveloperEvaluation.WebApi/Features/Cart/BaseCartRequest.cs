namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart;

public class BaseCartRequest
{
    public Guid UserId { get; set; }

    public ICollection<BaseCartProductApi> Products { get; set; } = [];
}
